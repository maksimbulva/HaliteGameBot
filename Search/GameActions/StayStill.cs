using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot.Search.GameActions
{
    sealed class StayStill : IGameAction
    {
        private readonly Ship _ship;
        private readonly int _haliteCollected;

        public StayStill(Game game, Ship ship)
        {
            _ship = ship;

            GameMap map = game.GameMap;
            int haliteInCellOldValue = map.GetHaliteAt(_ship);

            int haliteInCellNewValue = Math.Min(
                _ship.Halite + GameMechanicsHelper.HaliteToCollect(haliteInCellOldValue),
                Constants.MaxHalite);

            _haliteCollected = haliteInCellNewValue - _ship.Halite;
        }

        public void Play(Game game)
        {
            _ship.Halite += _haliteCollected;
            game.GameMap.ChangeHaliteAt(_ship, -_haliteCollected);
        }

        public void Undo(Game game)
        {
            _ship.Halite -= _haliteCollected;
            game.GameMap.SetHaliteAt(_ship, _haliteCollected);
        }
    }
}
