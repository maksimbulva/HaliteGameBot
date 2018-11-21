namespace HaliteGameBot.Framework
{
    static class GameMapGeometryUseCase
    {
        public static int IncCoord(int x, int maxCoord) => ++x < maxCoord ? x : x - maxCoord;

        public static int DecCoord(int x, int maxCoord) => --x >= 0 ? x : x + maxCoord;
    }
}
