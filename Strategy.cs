using HaliteGameBot.Search;

namespace HaliteGameBot
{
    internal class Strategy
    {
        public double EvaluateStatic(GameState gameState)
        {
            return 0.0;
        }

        public double GetPriority(double evaluation, int depth)
        {
            return evaluation / (depth + 1);
        }
    }
}
