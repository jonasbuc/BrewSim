using System.Collections.Generic;

namespace BrewSim
{
    public class BoundedBuffer<T>
    {
        private readonly Queue<T> _queue = new();
        private readonly int _capacity;
        private readonly object _lock = new();

        public int Capacity => _capacity;
        public int Count { get { lock (_lock) { return _queue.Count; } } }

        public BoundedBuffer(int capacity) => _capacity = capacity;

        public void Add(T item)
        {
            lock (_lock)
            {
                while (_queue.Count >= _capacity)
                    Monitor.Wait(_lock);
                _queue.Enqueue(item);
                Monitor.PulseAll(_lock);
            }
        }

        public T Remove()
        {
            lock (_lock)
            {
                while (_queue.Count == 0)
                    Monitor.Wait(_lock);
                var item = _queue.Dequeue();
                Monitor.PulseAll(_lock);
                return item;
            }
        }
    }
}