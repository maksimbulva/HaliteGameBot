using HaliteGameBot.Framework;
using HaliteGameBot.Search;
using System;

namespace HaliteGameBot
{
    internal class Strategy
    {
        private readonly Shipyard _myShipyard;

        public int BasePlayerHalite;

        public Strategy(Game game)
        {
            _myShipyard = game.MyPlayer.Shipyard;
        }

        public double EvaluateStatic(GameState gameState)
        {
            GameAction gameAction = gameState.RecentGameAction;
            if (gameAction == null)
            {
                return 0.0;
            }

            int distanceToShipyard = GameMapGeometry.NaiveDistance(
                _myShipyard.Position,
                new Position(gameAction.Ship.X, gameAction.Ship.Y));

            double shipHaliteBonus = Math.Max(0, 20 - distanceToShipyard) / 20.0 * gameAction.Ship.Halite;

            double result = gameAction.Player.Halite - BasePlayerHalite + shipHaliteBonus;

            return result;
        }
    }
}
