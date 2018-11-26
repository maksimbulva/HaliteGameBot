using HaliteGameBot.Search;

namespace HaliteGameBot
{
    internal class Strategy
    {
        public int BasePlayerHalite;

        public double EvaluateStatic(GameState gameState)
        {
            GameAction gameAction = gameState.RecentGameAction;
            if (gameAction == null)
            {
                return 0.0;
            }
            return gameAction.Player.Halite - BasePlayerHalite;
        }

        public double GetPriority(double evaluation, int depth)
        {
            return 0.33; // evaluation / (depth + 1);
        }
    }
}
