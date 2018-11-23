namespace HaliteGameBot.Framework.Commands
{
    internal sealed class BuildDropoffSite : ICommand
    {
        public int EntityId { get; }

        public BuildDropoffSite(int entityId) => EntityId = entityId;

        public override string ToString() => $"c {EntityId}";
    }
}
