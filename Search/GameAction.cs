using HaliteGameBot.Framework;
using HaliteGameBot.Search.Actors;
using System;

namespace HaliteGameBot.Search
{
    internal sealed class GameAction
    {
        // TODO - ActionType can be deduced from other fields
        // Remove it if under memory pressure
        public readonly GameActions ActionType;
        public readonly PlayerActor Player;
        public readonly ShipActor Ship;
        public readonly int OriginCellHalite;
        public readonly int OriginCellIndex;

        public static GameAction CreateStayStillAction(GameAction parent, int cellHalite, bool dropoff)
        {
            int newShipHalite = Math.Min(
                parent.Ship.Halite + GameMechanicsHelper.HaliteToCollect(cellHalite),
                Constants.MaxHalite);

            int haliteCollected = newShipHalite - parent.Ship.Halite;
            int cellIndex = GameMapGeometry.CellIndex(parent.Ship.X, parent.Ship.Y);

            if (dropoff)
            {
                return new GameAction(
                    GameActions.STAY_STILL,
                    parent.Player.WithHalite(parent.Player.Halite + newShipHalite),
                    parent.Ship.WithHalite(0),
                    cellHalite - haliteCollected,
                    cellIndex);
            }
            else
            {
                return new GameAction(
                    GameActions.STAY_STILL,
                    parent.Player,
                    parent.Ship.WithHalite(newShipHalite),
                    cellHalite - haliteCollected,
                    cellIndex);
            }
        }

        public static GameAction CreateMoveAction(
            GameActions actionType,
            GameAction parent,
            int newX,
            int newY,
            int originCellHalite,
            bool dropoff)
        {
            int moveCost = GameMechanicsHelper.HaliteToMoveCost(originCellHalite);
            if (moveCost > parent.Ship.Halite)
            {
                return null;
            }

            if (dropoff)
            {
                return new GameAction(
                    actionType,
                    parent.Player.WithHalite(parent.Player.Halite + parent.Ship.Halite - moveCost),
                    new ShipActor(newX, newY, 0),
                    originCellHalite,
                    GameMapGeometry.CellIndex(parent.Ship.X, parent.Ship.Y));
            }
            else
            {
                return new GameAction(
                    actionType,
                    parent.Player,
                    new ShipActor(newX, newY, parent.Ship.Halite - moveCost),
                    originCellHalite,
                    GameMapGeometry.CellIndex(parent.Ship.X, parent.Ship.Y));
            }
        }

        public static GameAction CreateRootAction(Game game, Ship ship)
        {
            int originCellIndex = game.GameMap.GetIndex(ship.Position);
            return new GameAction(
                GameActions.STAY_STILL,  // For root node this does not really matter
                new PlayerActor(game.MyPlayer.Halite),
                new ShipActor(ship.Position.X, ship.Position.Y, ship.Halite),
                game.GameMap.Halite[originCellIndex],
                originCellIndex);
        }

        private GameAction(
            GameActions actionType,
            PlayerActor player,
            ShipActor ship,
            int originCellHalite,
            int originCellIndex)
        {
            ActionType = actionType;
            Player = player;
            Ship = ship;
            OriginCellHalite = originCellHalite;
            OriginCellIndex = originCellIndex;
        }

        public override string ToString() => ActionType.ToString();
    }
}
