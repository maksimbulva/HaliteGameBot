namespace HaliteGameBot.Framework
{
    internal sealed class Ship : Entity
    {
        public int Halite { get; set; }

        public bool IsFull => Halite >= Constants.MaxHalite;

        public Ship(int playerId, int entityId, Position position, int halite)
            : base(playerId, entityId, position)
        {
            Halite = halite;
        }
    }
}
