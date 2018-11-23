using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class MoveX : IGameAction
    {
        public enum MoveDir
        {
            LEFT,
            RIGHT
        }

        private readonly Ship _ship;
        private readonly int _oldX;
        private readonly int _newX;

        public int MoveCost { get; }

        public int DeltaX => _newX - _oldX;

        public MoveX(GameMapState gameMapState, Ship ship, MoveDir moveDir)
        {
            _ship = ship;

            _oldX = _ship.Position.Y;
            _newX = moveDir == MoveDir.LEFT ? gameMapState.ToLeftOf(_oldX) : gameMapState.ToRightOf(_oldX);

            int haliteAtDestCell = gameMapState.GetHaliteAt(_newX, _ship.Position.Y);
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public void Play(GameMapState gameMapState)
        {
            _ship.Position = new Position(_newX, _ship.Position.Y);
            _ship.Halite -= MoveCost;
        }

        public void Undo(GameMapState gameMapState)
        {
            _ship.Position = new Position(_oldX, _ship.Position.Y);
            _ship.Halite += MoveCost;
        }
    }
}
