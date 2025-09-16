namespace BrewSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sim = new BrewerySimulation();
            sim.Start();
            Console.WriteLine("Simulation running. Press ENTER to exit.");
            Console.ReadLine();
            sim.Stop();
        }
    }
}