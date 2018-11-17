using System;

namespace HaliteGameBot.Framework
{
    class InputReader
    {
        private static readonly char[] separator = new char[] { ' ' };

        private readonly string[] tokens = Console.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
        private int nextTokenIndex = 0;

        public int NextInt() => int.Parse(tokens[nextTokenIndex++]);
    }
}
