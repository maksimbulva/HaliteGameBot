using HaliteGameBot.Framework;
using HaliteGameBot.Search.Actors;
using System;

namespace HaliteGameBot.Search
{
    internal sealed class GameAction
    {
        public readonly GameActions ActionType;
        public readonly PlayerActor Player;
        public readonly ShipActor Ship;
        public readonly int CellHalite;

        public static GameAction CreateStayStillAction(GameAction parent, bool dropoff)
        {
            int newCellHalite = Math.Min(
                parent.Ship.Halite + GameMechanicsHelper.HaliteToCollect(parent.CellHalite),
                Constants.MaxHalite);

            int haliteCollected = newCellHalite - parent.Ship.Halite;

            if (dropoff)
            {
                return new GameAction(
                    GameActions.STAY_STILL,
                    parent.Player.WithHalite(parent.Ship.Halite + haliteCollected),
                    parent.Ship.WithHalite(0),
                    newCellHalite);
            }
            else
            {
                return new GameAction(
                    GameActions.STAY_STILL,
                    parent.Player,
                    parent.Ship.WithHalite(parent.Ship.Halite + haliteCollected),
                    newCellHalite);
            }
        }

        public static GameAction CreateMoveAction(
            GameActions actionType,
            GameAction parent,
            int newX,
            int newY,
            int cellHalite,
            bool dropoff)
        {
            int moveCost = GameMechanicsHelper.HaliteToMoveCost(cellHalite);
            if (moveCost > parent.Player.Halite)
            {
                return null;
            }

            if (dropoff)
            {
                return new GameAction(
                    actionType,
                    parent.Player.WithHalite(parent.Player.Halite - moveCost + parent.Ship.Halite),
                    new ShipActor(newX, newY, 0),
                    cellHalite);
            }
            else
            {
                return new GameAction(
                    actionType,
                    parent.Player.WithHalite(parent.Player.Halite - moveCost),
                    parent.Ship.WithPosition(newX, newY),
                    cellHalite);
            }
        }

        public static GameAction CreateRootAction(Game game, Ship ship)
        {
            return new GameAction(
                GameActions.STAY_STILL,  // For root node this does not really matter
                new PlayerActor(game.MyPlayer.Halite),
                new ShipActor(ship.Position.X, ship.Position.Y, ship.Halite),
                game.GameMap.GetHaliteAt(ship));
        }

        private GameAction(GameActions actionType, PlayerActor player, ShipActor ship, int cellHalite)
        {
            ActionType = actionType;
            Player = player;
            Ship = ship;
            CellHalite = cellHalite;
        }

        public override string ToString() => ActionType.ToString();
    }
}
