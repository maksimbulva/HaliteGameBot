namespace HaliteGameBot.Framework
{
    abstract class Entity
    {
        protected Entity(int playerId, int entityId, Position position)
        {
            PlayerId = playerId;
            EntityId = entityId;
            Position = position;
        }

        public int PlayerId { get; protected set; }
        public int EntityId { get; protected set; }
        public Position Position { get; set; }
    }
}
