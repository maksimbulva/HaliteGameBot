using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot.Search.GameActions
{
    internal sealed class StayStill : BaseShipAction
    {
        private readonly int _haliteCollected;

        public override Position Destination => _ship.Position;

        public StayStill(GameMapState gameMapState, Ship ship) : base(ship)
        {
            int haliteInCellOldValue = gameMapState.GetHaliteAt(_ship);

            int haliteInCellNewValue = Math.Min(
                _ship.Halite + GameMechanicsHelper.HaliteToCollect(haliteInCellOldValue),
                Constants.MaxHalite);

            _haliteCollected = haliteInCellNewValue - _ship.Halite;
        }

        public override void Play(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Halite += _haliteCollected;
            gameMapState.ChangeHaliteAt(_ship, -_haliteCollected);
            TryDropoff(playerState, gameMapState);
        }

        public override void Undo(PlayerState playerState, GameMapState gameMapState)
        {
            _ship.Halite -= _haliteCollected;
            gameMapState.ChangeHaliteAt(_ship, _haliteCollected);
            UndoDropoff(playerState, gameMapState);
        }

        public override string ToString() => "STAY";
    }
}
