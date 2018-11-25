using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal abstract class BaseShipAction : IGameAction
    {
        protected readonly Ship _ship;
        protected int _droppedOff;

        protected BaseShipAction(Ship ship)
        {
            _ship = ship;
        }

        public abstract Position Destination { get; }

        public abstract void Play(PlayerState playerState, GameMapState gameMapState);
        public abstract void Undo(PlayerState playerState, GameMapState gameMapState);

        protected void TryDropoff(PlayerState playerState, GameMapState gameMapState)
        {
            if (gameMapState.IsDropoffAt(_ship))
            {
                _droppedOff = _ship.Halite;
                _ship.Halite = 0;
                playerState.Halite += _droppedOff;
            }
        }

        protected void UndoDropoff(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Halite += _droppedOff;
            playerState.Halite -= _droppedOff;
        }
    }
}
