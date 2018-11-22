using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    sealed class MoveX : IGameAction
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

        public MoveX(Game game, Ship ship, MoveDir moveDir)
        {
            _ship = ship;

            GameMap map = game.GameMap;
            _oldX = _ship.Position.Y;
            _newX = moveDir == MoveDir.LEFT
                ? GameMapGeometryUseCase.DecCoord(_oldX, map.Width)
                : GameMapGeometryUseCase.IncCoord(_oldX, map.Width);

            int haliteAtDestCell = map.Halite[map.GetIndex(_newX, _ship.Position.Y)];
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public void Play(Game game)
        {
            _ship.Position = new Position(_newX, _ship.Position.Y);
            _ship.Halite -= MoveCost;
        }

        public void Undo(Game game)
        {
            _ship.Position = new Position(_oldX, _ship.Position.Y);
            _ship.Halite += MoveCost;
        }
    }
}
