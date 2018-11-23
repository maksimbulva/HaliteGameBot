using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class MoveY : IGameAction
    {
        public enum MoveDir
        {
            NORTH,
            SOUTH
        }

        private readonly Ship _ship;
        private readonly int _oldY;
        private readonly int _newY;

        public int MoveCost { get; }

        public int DeltaY => _newY - _oldY;

        public MoveY(GameMapState _gameMapState, Ship ship, MoveDir moveDir)
        {
            _ship = ship;

            _oldY = _ship.Position.Y;
            _newY = moveDir == MoveDir.NORTH ? _gameMapState.ToNorthOf(_oldY) : _gameMapState.ToSouthOf(_oldY);

            int haliteAtDestCell = _gameMapState.GetHaliteAt(_ship.Position.X, _newY);
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public void Play(GameMapState _gameMapState)
        {
            _ship.Position = new Position(_ship.Position.X, _newY);
            _ship.Halite -= MoveCost;
        }

        public void Undo(GameMapState _gameMapState)
        {
            _ship.Position = new Position(_ship.Position.X, _oldY);
            _ship.Halite += MoveCost;
        }
    }
}
