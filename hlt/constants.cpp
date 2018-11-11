#include "constants.hpp"
#include "input.hpp"
#include "log.hpp"

#include <unordered_map>
#include <sstream>
#include <vector>

namespace {

bool strToBool(const std::string& str) {
    if (str == "true") {
        return true;
    }
    else if (str == "false") {
        return false;
    }
    else
    {
        hlt::log::log("Cannot convert \'" + str + "\' to bool.");
        exit(1);
        return false;
    }
}

void replacePunctuationCharsWithSpaces(std::string& str) {
    for (auto it = str.begin(); it != str.end(); ++it) {
        const char c = *it;
        if (c == '{' || c == '}' || c == ',' || c == ':' || c == '\"') {
            *it = ' ';
        }
    }
}

}  // namespace

namespace hlt {

void Constants::init(std::string inputString) {

    log::log("Constants string; " + inputString);
    
    replacePunctuationCharsWithSpaces(inputString);

    size_t i = 0;
    while (i < inputString.size()) {
        const std::string keyStr = readToken(inputString, i);
        if (keyStr.empty()) {
            break;
        }

        const std::string valueStr = readToken(inputString, i);
        if (valueStr.empty()) {
            log::log("Missing token while reading value for constant key " + std::string{ keyStr } + ".");
            exit(1);
        }

        // log::log("Set constant: key=" + std::string{ keyStr } + ", value=" + valueStr);

        if (keyStr == "NEW_ENTITY_ENERGY_COST") {
            m_shipCost = stoi(valueStr);
        }
        else if (keyStr == "DROPOFF_COST") {
            m_dropoffCost = stoi(valueStr);
        }
        else if (keyStr == "MAX_ENERGY") {
            m_maxHalite = stoi(valueStr);
        }
        else if (keyStr == "MAX_TURNS") {
            m_maxTurnCount = stoi(valueStr);
        }
        else if (keyStr == "EXTRACT_RATIO") {
            m_extractRatio = stoi(valueStr);
        }
        else if (keyStr == "MOVE_COST_RATIO") {
            m_moveCostRatio = stoi(valueStr);
        }
        else if (keyStr == "INSPIRATION_ENABLED") {
            m_inspirationEnabled = strToBool(valueStr);
        }
        else if (keyStr == "INSPIRATION_RADIUS") {
            m_inspirationRadius = stoi(valueStr);
        }
        else if (keyStr == "INSPIRATION_SHIP_COUNT") {
            m_inspirationShipCount = stoi(valueStr);
        }
        else if (keyStr == "INSPIRED_EXTRACT_RATIO") {
            m_inspiredExtractRatio = stoi(valueStr);
        }
        else if (keyStr == "INSPIRED_BONUS_MULTIPLIER") {
            m_inspiredBonusMultiplier = stod(valueStr);
        }
        else if (keyStr == "INSPIRED_MOVE_COST_RATIO") {
            m_inspiredMoveCostRatio = stoi(valueStr);
        }
        else if (keyStr == "CAPTURE_ENABLED") {
            // TODO
        }
        else if (keyStr == "CAPTURE_RADIUS") {
            // TODO
        }
        else if (keyStr == "DEFAULT_MAP_HEIGHT") {
            // TODO
        }
        else if (keyStr == "DEFAULT_MAP_WIDTH") {
            // TODO
        }
        else if (keyStr == "DROPOFF_PENALTY_RATIO") {
            // TODO
        }
        else if (keyStr == "FACTOR_EXP_1") {
            // TODO
        }
        else if (keyStr == "FACTOR_EXP_2") {
            // TODO
        }
        else if (keyStr == "INITIAL_ENERGY") {
            // TODO
        }
        else if (keyStr == "MAX_CELL_PRODUCTION") {
            // TODO
        }
        else if (keyStr == "MAX_PLAYERS") {
            // TODO
        }
        else if (keyStr == "MAX_TURN_THRESHOLD") {
            // TODO
        }
        else if (keyStr == "MIN_CELL_PRODUCTION") {
            // TODO
        }
        else if (keyStr == "MIN_TURNS") {
            // TODO
        }
        else if (keyStr == "MIN_TURN_THRESHOLD") {
            // TODO
        }
        else if (keyStr == "PERSISTENCE") {
            // TODO
        }
        else if (keyStr == "SHIPS_ABOVE_FOR_CAPTURE") {
            // TODO
        }
        else if (keyStr == "STRICT_ERRORS") {
            // TODO
        }
        else if (keyStr == "game_seed") {
            // TODO
        }
        else {
            log::log("Unexpected const key token \'" + std::string{ keyStr } + "\'.");
            // exit(1);
        }
    }
}

}  // namespace hlt
