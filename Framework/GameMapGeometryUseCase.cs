namespace HaliteGameBot.Framework
{
    internal static class GameMapGeometryUseCase
    {
        public static int IncCoord(int c, int maxCoord) => ++c < maxCoord ? c : c - maxCoord;

        public static int DecCoord(int c, int maxCoord) => --c >= 0 ? c : c + maxCoord;
    }
}
