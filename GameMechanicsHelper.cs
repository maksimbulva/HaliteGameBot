using HaliteGameBot.Framework;
using System;

namespace HaliteGameBot
{
    static class GameMechanicsHelper
    {
        static private double InvExtractRatio;

        static public void Init()
        {
            InvExtractRatio = 1.0 / Constants.InspiredExtractRatio;
        }

        // Convert.ToInt32(haliteInCell * InvExtractRatio)
        static public int HaliteToCollect(int haliteInCell) => haliteInCell / Constants.ExtractRatio;

        static public int HaliteToMoveCost(int haliteInDestCell) => (haliteInDestCell + 5) / 10;
    }
}
