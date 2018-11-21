using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot.Search.GameActions
{
    sealed class StayStill : IGameAction
    {
        private readonly Ship _ship;
        private int _haliteInCellOldValue;
        private int _haliteOnShipOldValue;

        public StayStill(Ship ship)
        {
            _ship = ship;
        }

        public void Play(Game game)
        {
            _haliteOnShipOldValue = _ship.Halite;
            GameMap map = game.GameMap;
            _haliteInCellOldValue = map.GetHaliteAt(_ship);
            // TODO: This is not a correct formula (we need to do calculations in float numbers
            // and then round up to the nearest integer)
            // But for now, this should be insignificant
            int haliteCollected = _haliteInCellOldValue / Constants.ExtractRatio;
            if (_ship.Halite + haliteCollected > Constants.MaxHalite)
            {
                haliteCollected = Math.Max(0, Constants.MaxHalite - _ship.Halite);
            }
            _ship.Halite += haliteCollected;
            map.ChangeHaliteAt(_ship, -haliteCollected);
        }

        public void Undo(Game game)
        {
            game.GameMap.SetHaliteAt(_ship, _haliteInCellOldValue);
            _ship.Halite = _haliteOnShipOldValue;
        }
    }
}
