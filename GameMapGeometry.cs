using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot
{
    internal static class GameMapGeometry
    {
        public static int GameMapWidth { get; private set; }
        public static int GameMapHeight { get; private set; }

        public static int NaiveDistance(Position p1, Position p2)
        {
            int deltaX = Math.Abs(p1.X - p2.X);
            deltaX = Math.Min(deltaX, GameMapWidth - deltaX);
            int deltaY = Math.Abs(p1.Y - p2.Y);
            deltaY = Math.Min(deltaY, GameMapHeight - deltaY);
            return deltaX + deltaY;
        }

        public static void InitDimensions(GameMap gameMap)
        {
            GameMapWidth = gameMap.Width;
            GameMapHeight = gameMap.Height;
        }

        public static int CellIndex(int x, int y) => x + y * GameMapWidth;
    }
}
