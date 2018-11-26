namespace HaliteGameBot.Search.Actors
{
    internal struct PlayerActor
    {
        public readonly int Halite;

        public PlayerActor(int halite)
        {
            Halite = halite;
        }

        public PlayerActor WithHalite(int halite)
        {
            return new PlayerActor(halite);
        }
    }
}
