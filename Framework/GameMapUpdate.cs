namespace HaliteGameBot.Framework
{
    internal struct GameMapUpdate
    {
        public int CellIndex { get; }
        public int Halite { get; }

        public GameMapUpdate(int cellIndex, int halite)
        {
            CellIndex = cellIndex;
            Halite = halite;
        }
    }
}
