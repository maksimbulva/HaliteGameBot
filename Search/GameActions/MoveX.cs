using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class MoveX : BaseShipAction
    {
        public enum MoveDir
        {
            LEFT,
            RIGHT
        }

        private readonly int _oldX;
        private readonly int _newX;

        public int MoveCost { get; }

        public int DeltaX => _newX - _oldX;

        public override Position Destination => new Position(_newX, _ship.Position.Y);

        public MoveX(GameMapState gameMapState, Ship ship, MoveDir moveDir) : base(ship)
        {
            _oldX = _ship.Position.Y;
            _newX = moveDir == MoveDir.LEFT ? gameMapState.ToLeftOf(_oldX) : gameMapState.ToRightOf(_oldX);

            int haliteAtDestCell = gameMapState.GetHaliteAt(_newX, _ship.Position.Y);
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public override void Play(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Position = new Position(_newX, _ship.Position.Y);
            playerState.Halite -= MoveCost;
            TryDropoff(playerState, gameMapState);
        }

        public override void Undo(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Position = new Position(_oldX, _ship.Position.Y);
            playerState.Halite += MoveCost;
            UndoDropoff(playerState, gameMapState);
        }

        public override string ToString() => DeltaX == -1 || DeltaX > 1 ? "WEST" : "EAST";
    }
}
