using System;
using System.Globalization;

namespace HaliteGameBot.Framework
{
    /// <summary>
    /// The constants representing the game variation being played.
    /// They come from game engine and changing them has no effect.
    /// They are strictly informational.
    /// </summary>
    static class Constants
    {
        // The cost to build a single ship.
        public static int ShipCost { get; private set; }

        // The cost to build a dropoff.
        public static int DropoffCost { get; private set; }

        // The maximum amount of halite a ship can carry.
        public static int MaxHalite { get; private set; }

        // The maximum number of turns a game can last.
        public static int MaxTurnCount { get; private set; }

        // 1/EXTRACT_RATIO halite (rounded) is collected from a square per turn.
        public static int ExtractRatio { get; private set; }

        // 1/MOVE_COST_RATIO halite (rounded) is needed to move off a cell.
        public static int MoveCostRatio { get; private set; }

        // Whether inspiration is enabled.
        public static bool InspirationEnabled { get; private set; }

        // A ship is inspired if at least InspirationShipCount opponent ships are within this Manhattan distance.
        public static int InspirationRadius { get; private set; }

        // A ship is inspired if at least this many opponent ships are within InspirationRadius distance.
        public static int InspirationShipCount { get; private set; }

        // An inspired ship mines 1/X halite from a cell per turn instead.
        public static int InspiredExtractRatio { get; private set; }

        // An inspired ship that removes Y halite from a cell collects X*Y additional halite.
        public static double InspiredBonusMultiplier { get; private set; }

        // An inspired ship instead spends 1/X% halite to move.
        public static int InspiredMoveCostRatio { get; private set; }

        public static void Init(string inputString)
        {
            string[] tokens = inputString.Split(new char[] { ' ', '{', '}', ',', ':', '\"' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length % 2 != 0)
            {
                throw new ArgumentException("Failed to read constant values for the specified constant keys");
            }

            for (int i = 0; i < tokens.Length; i += 2)
            {
                string key = tokens[i];
                string value = tokens[i + 1];
                switch (key)
                {
                    case "NEW_ENTITY_ENERGY_COST":
                        ShipCost = int.Parse(value);
                        break;
                    case "DROPOFF_COST":
                        DropoffCost = int.Parse(value);
                        break;
                    case "MAX_ENERGY":
                        MaxHalite = int.Parse(value);
                        break;
                    case "MAX_TURNS":
                        MaxTurnCount = int.Parse(value);
                        break;
                    case "EXTRACT_RATIO":
                        ExtractRatio = int.Parse(value);
                        break;
                    case "MOVE_COST_RATIO":
                        MoveCostRatio = int.Parse(value);
                        break;
                    case "INSPIRATION_ENABLED":
                        InspirationEnabled = bool.Parse(value);
                        break;
                    case "INSPIRATION_RADIUS":
                        InspirationRadius = int.Parse(value);
                        break;
                    case "INSPIRATION_SHIP_COUNT":
                        InspirationShipCount = int.Parse(value);
                        break;
                    case "INSPIRED_EXTRACT_RATIO":
                        InspiredExtractRatio = int.Parse(value);
                        break;
                    case "INSPIRED_BONUS_MULTIPLIER":
                        InspiredBonusMultiplier = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                        break;
                    case "INSPIRED_MOVE_COST_RATIO":
                        InspiredMoveCostRatio = int.Parse(value);
                        break;
                    case "CAPTURE_ENABLED":
                    case "CAPTURE_RADIUS":
                    case "DEFAULT_MAP_HEIGHT":
                    case "DEFAULT_MAP_WIDTH":
                    case "DROPOFF_PENALTY_RATIO":
                    case "FACTOR_EXP_1":
                    case "FACTOR_EXP_2":
                    case "INITIAL_ENERGY":
                    case "MAX_CELL_PRODUCTION":
                    case "MAX_PLAYERS":
                    case "MAX_TURN_THRESHOLD":
                    case "MIN_CELL_PRODUCTION":
                    case "MIN_TURNS":
                    case "MIN_TURN_THRESHOLD":
                    case "PERSISTENCE":
                    case "SHIPS_ABOVE_FOR_CAPTURE":
                    case "STRICT_ERRORS":
                    case "game_seed":
                        // TODO
                        break;
                    default:
                        Log.Write($"Unexpected const key token '{key}'.");
                        break;
                }
            }
        }
    }
}
