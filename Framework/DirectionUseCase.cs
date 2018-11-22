using System;

namespace HaliteGameBot.Framework
{
    static class DirectionUseCase
    {
        public static readonly Direction[] AllCardinals = new Direction[4]
        {
            Direction.WEST,
            Direction.EAST,
            Direction.NORTH,
            Direction.SOUTH,
        };

        public static char ToChar(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return 'n';
                case Direction.EAST:
                    return 'e';
                case Direction.SOUTH:
                    return 's';
                case Direction.WEST:
                    return 'w';
                case Direction.STAY_STILL:
                    return 'o';
                default:
                    throw new ArgumentException($"Direction {direction} is not supposed to be converted to char");
            }
        }
    }
}
