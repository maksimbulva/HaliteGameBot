using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class MoveY : BaseShipAction
    {
        public enum MoveDir
        {
            NORTH,
            SOUTH
        }

        private readonly int _oldY;
        private readonly int _newY;

        public int MoveCost { get; }

        public int DeltaY => _newY - _oldY;

        public override Position Destination => new Position(_ship.Position.X, _newY);

        public MoveY(GameMapState gameMapState, Ship ship, MoveDir moveDir) : base(ship)
        {
            _oldY = _ship.Position.Y;
            _newY = moveDir == MoveDir.NORTH ? gameMapState.ToNorthOf(_oldY) : gameMapState.ToSouthOf(_oldY);

            int haliteAtDestCell = gameMapState.GetHaliteAt(_ship.Position.X, _newY);
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public override void Play(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Position = new Position(_ship.Position.X, _newY);
            playerState.Halite -= MoveCost;
            TryDropoff(playerState, gameMapState);
        }

        public override void Undo(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Position = new Position(_ship.Position.X, _oldY);
            playerState.Halite += MoveCost;
            UndoDropoff(playerState, gameMapState);
        }

        public override string ToString() => DeltaY == -1 || DeltaY > 1 ? "NORTH" : "SOUTH";
    }
}
