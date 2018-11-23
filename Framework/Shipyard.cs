namespace HaliteGameBot.Framework
{
    internal sealed class Shipyard : Entity
    {
        public const int ENTITY_ID = -1;

        public Shipyard(int playerId, Position position)
            : base(playerId, ENTITY_ID, position)
        {
        }
    }
}
