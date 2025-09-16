using System;
using System.Collections.Generic;
using System.Threading;

namespace BrewSim
{
    public class BrewerySimulation
    {
        public event Action? StateChanged;

        public int WashedCount => washedBuffer.Count;
        public int FilledCount => filledBuffer.Count;
        public int ToppedCount => toppedBuffer.Count;
        public int BoxedCount => boxedBuffer.Count;

        // Call this method after every buffer change:
        private void OnStateChanged() => StateChanged?.Invoke();

        private readonly BoundedBuffer<Bottle> washedBuffer = new(6);
        private readonly BoundedBuffer<Bottle> filledBuffer = new(6);
        private readonly BoundedBuffer<Bottle> toppedBuffer = new(24);
        private readonly BoundedBuffer<List<Bottle>> boxedBuffer = new(10);

        private readonly List<Thread> threads = new();
        private volatile bool running = false;
        private int bottleId = 0;

        public void Start()
        {
            running = true;
            threads.Add(new Thread(BottleGenerator) { IsBackground = true });
            for (int i = 0; i < 3; i++)
                threads.Add(new Thread(() => WashingMachine(i + 1)) { IsBackground = true });
            for (int i = 0; i < 6; i++)
                threads.Add(new Thread(() => FillingMachine(i + 1)) { IsBackground = true });
            for (int i = 0; i < 6; i++)
                threads.Add(new Thread(() => ToppingMachine(i + 1)) { IsBackground = true });
            for (int i = 0; i < 2; i++)
                threads.Add(new Thread(() => BoxingMachine(i + 1)) { IsBackground = true });

            foreach (var t in threads) t.Start();
        }

        public void Stop()
        {
            running = false;
            foreach (var t in threads)
                if (t.IsAlive) t.Join(500);
        }

        private void BottleGenerator()
        {
            var rand = new Random();
            while (running)
            {
                Thread.Sleep(rand.Next(100, 400));
                var bottle = new Bottle(Interlocked.Increment(ref bottleId));
                Console.WriteLine($"[Generator] {bottle}");
                washedBuffer.Add(bottle);
                OnStateChanged();
            }
        }

        private void WashingMachine(int machineId)
        {
            var rand = new Random(machineId * 1000);
            while (running)
            {
                var bottle = washedBuffer.Remove();
                OnStateChanged();
                Console.WriteLine($"[Washer {machineId}] Washing {bottle}");
                Thread.Sleep(rand.Next(200, 500));
                filledBuffer.Add(bottle);
                OnStateChanged();
                Console.WriteLine($"[Washer {machineId}] Washed {bottle}");
            }
        }

        private void FillingMachine(int machineId)
        {
            var rand = new Random(machineId * 2000);
            while (running)
            {
                var bottle = filledBuffer.Remove();
                OnStateChanged();
                // Only fill if topping is ready (simulate with buffer space)
                lock (toppedBuffer)
                {
                    while (toppedBuffer.Count >= toppedBuffer.Capacity && running)
                        Monitor.Wait(toppedBuffer);
                    Console.WriteLine($"[Filler {machineId}] Filling {bottle}");
                    Thread.Sleep(rand.Next(200, 400));
                    toppedBuffer.Add(bottle);
                    OnStateChanged();
                    Monitor.PulseAll(toppedBuffer);
                    Console.WriteLine($"[Filler {machineId}] Filled {bottle}");
                }
            }
        }

        private void ToppingMachine(int machineId)
        {
            var rand = new Random(machineId * 3000);
            while (running)
            {
                var bottle = toppedBuffer.Remove();
                OnStateChanged();
                Console.WriteLine($"[Topper {machineId}] Topping {bottle}");
                Thread.Sleep(rand.Next(150, 350));
                BoxingCollector.Add(bottle, boxedBuffer, OnStateChanged);
                Console.WriteLine($"[Topper {machineId}] Topped {bottle}");
            }
        }

        private void BoxingMachine(int machineId)
        {
            var rand = new Random(machineId * 4000);
            while (running)
            {
                var box = boxedBuffer.Remove();
                OnStateChanged();
                Console.WriteLine($"[Boxer {machineId}] Boxing 24 bottles: {string.Join(", ", box.ConvertAll(b => b.Id))}");
                Thread.Sleep(rand.Next(500, 1000));
                Console.WriteLine($"[Boxer {machineId}] Boxed 24 bottles.");
            }
        }

        // Helper for collecting bottles into boxes of 24
        private static class BoxingCollector
        {
            private static List<Bottle> currentBox = new(24);
            private static readonly object boxLock = new();

            public static void Add(Bottle bottle, BoundedBuffer<List<Bottle>> boxedBuffer, Action onStateChanged)
            {
                lock (boxLock)
                {
                    currentBox.Add(bottle);
                    if (currentBox.Count == 24)
                    {
                        boxedBuffer.Add(new List<Bottle>(currentBox));
                        onStateChanged?.Invoke();
                        currentBox.Clear();
                    }
                }
            }
        }
    }
}