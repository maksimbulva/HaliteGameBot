#pragma once

#include <string>

namespace hlt {
/**
 * The constants representing the game variation being played.
 * They come from game engine and changing them has no effect.
 * They are strictly informational.
 */
class Constants {
public:
    static Constants& get() {
        static Constants instance;
        return instance;
    }

    /** The cost to build a single ship. */
    const fastint shipCost() const { return m_shipCost; }

    /** The cost to build a dropoff. */
    const fastint dropoffCost() const { return m_dropoffCost; }

    /** The maximum amount of halite a ship can carry. */
    const fastint maxHalite() const { return m_maxHalite; }

    /** The maximum number of turns a game can last. */
    const fastint maxTurnCount() const { return m_maxTurnCount; }

    /** 1/EXTRACT_RATIO halite (rounded) is collected from a square per turn. */
    const fastint extractRatio() const { return m_extractRatio; }

    /** 1/MOVE_COST_RATIO halite (rounded) is needed to move off a cell. */
    const fastint moveCostRatio() const { return m_moveCostRatio; }

    /** Whether inspiration is enabled. */
    const bool inspirationEnabled() const { return m_inspirationEnabled; }

    /** A ship is inspired if at least INSPIRATION_SHIP_COUNT opponent ships are within this Manhattan distance. */
    const fastint inspirationRadius() const { return m_inspirationRadius; }

    /** A ship is inspired if at least this many opponent ships are within INSPIRATION_RADIUS distance. */
    const fastint inspirationShipCount() const { return m_inspirationShipCount; }

    /** An inspired ship mines 1/X halite from a cell per turn instead. */
    const fastint inspiredExtractRatio() const { return m_inspiredExtractRatio; }

    /** An inspired ship that removes Y halite from a cell collects X*Y additional halite. */
    const double inspiredBonusMultiplier() const { return m_inspiredBonusMultiplier; }

    /** An inspired ship instead spends 1/X% halite to move. */
    const fastint inspiredMoveCostRatio() const { return m_inspiredMoveCostRatio; }

private:
    friend struct Game;

    void init(std::string inputString);

    fastint m_shipCost;
    fastint m_dropoffCost;
    fastint m_maxHalite;
    fastint m_maxTurnCount;
    fastint m_extractRatio;
    fastint m_moveCostRatio;
    bool m_inspirationEnabled;
    fastint m_inspirationRadius;
    fastint m_inspirationShipCount;
    fastint m_inspiredExtractRatio;
    double m_inspiredBonusMultiplier;
    fastint m_inspiredMoveCostRatio;
};

}  // namespace hlt
