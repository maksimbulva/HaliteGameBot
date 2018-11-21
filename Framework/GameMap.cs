using System;
using System.Collections.Generic;
using System.Linq;

namespace HaliteGameBot.Framework
{
    class GameMap
    {
        public int Width { get; }
        public int Height { get; }

        public List<int> Halite { get; }
        public List<bool> Occupied { get; }

        public static GameMap CreateFromInput()
        {
            var reader = new InputReader();
            int width = reader.NextInt();
            int height = reader.NextInt();

            Log.Write($"Generate map of size width={width}, height={height}");

            var map = new GameMap(width, height);
            for (int y = 0; y < height; ++y)
            {
                var mapRowReader = new InputReader();
                for (int x = 0; x < width; ++x)
                {
                    map.Halite[map.GetIndex(x, y)] = mapRowReader.NextInt();
                }
            }

            // TODO log::log("Finished reading map");

            return map;
        }

        public GameMap(int width, int height)
        {
            Width = width;
            Height = height;

            int mapCellCount = width * height;
            Halite = new List<int>(mapCellCount);
            Halite.AddRange(Enumerable.Repeat(0, mapCellCount));
            Occupied = new List<bool>(mapCellCount);
            Occupied.AddRange(Enumerable.Repeat(false, mapCellCount));
        }

        public void UpdateFromInput()
        {
            var reader = new InputReader();
            for (int updateCount = reader.NextInt(); updateCount > 0; --updateCount)
            {
                var updateReader = new InputReader();
                int x = updateReader.NextInt();
                int y = updateReader.NextInt();
                int halite = updateReader.NextInt();
                Halite[GetIndex(x, y)] = halite;
            }
        }

        public int GetIndex(int x, int y) => x + y * Width;

        public int GetIndex(Position position) => position.X + position.Y * Width;

        public void SetHaliteAt(Entity entity, int value)
        {
            Halite[GetIndex(entity.Position.X, entity.Position.Y)] = value;
        }

        public int GetHaliteAt(Entity entity) => Halite[GetIndex(entity.Position.X, entity.Position.Y)];

        public void ChangeHaliteAt(Entity entity, int diff)
        {
            int index = GetIndex(entity.Position.X, entity.Position.Y);
            int halite = Halite[index];
            Halite[index] = Math.Max(0, halite + diff);
        }
    }
}
