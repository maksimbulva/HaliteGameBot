using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class StayStill : IGameAction
    {
        private readonly Ship _ship;
        private readonly int _haliteCollected;

        public StayStill(GameMapState gameMapState, Ship ship)
        {
            _ship = ship;

            int haliteInCellOldValue = gameMapState.GetHaliteAt(_ship);

            int haliteInCellNewValue = Math.Min(
                _ship.Halite + GameMechanicsHelper.HaliteToCollect(haliteInCellOldValue),
                Constants.MaxHalite);

            _haliteCollected = haliteInCellNewValue - _ship.Halite;
        }

        public void Play(GameMapState gameMapState)
        {
            _ship.Halite += _haliteCollected;
            gameMapState.ChangeHaliteAt(_ship, -_haliteCollected);
        }

        public void Undo(GameMapState gameMapState)
        {
            _ship.Halite -= _haliteCollected;
            gameMapState.ChangeHaliteAt(_ship, _haliteCollected);
        }
    }
}
