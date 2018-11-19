using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaliteGameBot.Search;

namespace HaliteGameBot
{
    class Strategy
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
