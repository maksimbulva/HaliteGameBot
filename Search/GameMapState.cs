using HaliteGameBot.Framework;
using System;
using System.Collections.Generic;

namespace HaliteGameBot.Search
{
    internal sealed class GameMapState
    {
        public int Width { get; }
        public int Height { get; }

        public int[] Halite { get; }

        public HashSet<int> Dropoffs;

        public GameMapState(Game game)
        {
            GameMap gameMap = game.GameMap;
            Width = gameMap.Width;
            Height = gameMap.Height;

            int[] sourceHalite = gameMap.Halite;
            Halite = new int[sourceHalite.Length];
            for (int i = 0; i < sourceHalite.Length; ++i)
            {
                Halite[i] = sourceHalite[i];
            }
        }

        public int ToNorthOf(int y) => --y >= 0 ? y : y + Height;
        public int ToSouthOf(int y) => ++y < Height ? y : 0;

        public int ToLeftOf(int x) => --x >= 0 ? x : x + Width;
        public int ToRightOf(int x) => ++x < Width ? x : 0;

        public int GetCellIndex(int x, int y) => x + y * Width;
        public int GetCellIndex(Position position) => position.X + position.Y * Width;

        public int GetHaliteAt(int x, int y) => Halite[x + y * Width];
        public int GetHaliteAt(Position position) => Halite[position.X + position.Y * Width];
        public int GetHaliteAt(Ship ship) => Halite[ship.Position.X + ship.Position.Y * Width];

        public void ChangeHaliteAt(Ship ship, int diff)
        {
            int index = GetCellIndex(ship.Position.X, ship.Position.Y);
            int halite = Halite[index];
            Halite[index] = Math.Max(0, halite + diff);
        }

        public void ApplyUpdates(IEnumerable<GameMapUpdate> mapUpdates)
        {
            foreach (GameMapUpdate update in mapUpdates)
            {
                Halite[update.CellIndex] = update.Halite;
            }
        }

        public void ApplyUpdate(GameMapUpdate mapUpdate) => Halite[mapUpdate.CellIndex] = mapUpdate.Halite;

        public bool IsDropoffAt(Ship ship) => Dropoffs.Contains(ship.Position.X + ship.Position.Y * Width);
        public bool IsDropoffAt(Position position) => Dropoffs.Contains(position.X + position.Y * Width);

        public bool IsDropoffAt(GameAction gameAction)
        {
            int x = gameAction.Ship.X;
            int y = gameAction.Ship.Y;
            return Dropoffs.Contains(x + y * Width);
        }
    }
}
