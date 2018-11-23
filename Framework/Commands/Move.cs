namespace HaliteGameBot.Framework.Commands
{
    internal sealed class Move : ICommand
    {
        public int EntityId { get; }
        public Direction Direction { get; }

        public Move(int entityId, Direction direction)
        {
            EntityId = entityId;
            Direction = direction;
        }

        public override string ToString() => $"m {EntityId} {DirectionUseCase.ToChar(Direction)}";
    }
}
