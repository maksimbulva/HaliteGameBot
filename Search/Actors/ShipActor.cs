namespace HaliteGameBot.Search.Actors
{
    internal struct ShipActor
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Halite;

        public ShipActor(int x, int y, int halite)
        {
            X = x;
            Y = y;
            Halite = halite;
        }

        public ShipActor WithPosition(int x, int y) => new ShipActor(x, y, Halite);

        public ShipActor WithHalite(int halite) => new ShipActor(X, Y, halite);
    }
}
