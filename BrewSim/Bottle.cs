namespace BrewSim
{
    public class Bottle
    {
        public int Id { get; }
        public Bottle(int id) => Id = id;
        public override string ToString() => $"Bottle {Id}";
    }
}