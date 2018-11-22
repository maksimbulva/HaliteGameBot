using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    sealed class MoveY : IGameAction
    {
        public enum MoveDir
        {
            UP,
            DOWN
        }

        private readonly Ship _ship;
        private readonly int _oldY;
        private readonly int _newY;

        public int MoveCost { get; }

        public int DeltaY => _newY - _oldY;

        public MoveY(Game game, Ship ship, MoveDir moveDir)
        {
            _ship = ship;

            GameMap map = game.GameMap;
            _oldY = _ship.Position.Y;
            _newY = moveDir == MoveDir.UP
                ? GameMapGeometryUseCase.DecCoord(_oldY, map.Height)
                : GameMapGeometryUseCase.IncCoord(_oldY, map.Height);

            int haliteAtDestCell = map.Halite[map.GetIndex(_ship.Position.X, _newY)];
            MoveCost = GameMechanicsHelper.HaliteToMoveCost(haliteAtDestCell);
        }

        public void Play(Game game)
        {
            _ship.Position = new Position(_ship.Position.X, _newY);
            _ship.Halite -= MoveCost;
        }

        public void Undo(Game game)
        {
            _ship.Position = new Position(_ship.Position.X, _oldY);
            _ship.Halite += MoveCost;
        }
    }
}
