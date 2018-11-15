#include "player.hpp"

#include "factory.hpp"

namespace hlt {

Player::Player(const PlayerId player_id, const Position shipyard) :
    m_playerId(player_id),
    m_halite(0),
    m_shipyard(player_id, shipyard)
{
}

void hlt::Player::_update(int num_ships, int num_dropoffs, Halite halite) {
    m_halite = halite;

    m_ships.clear();
    m_ships.reserve(num_ships);
    for (; num_ships > 0; --num_ships) {
        m_ships.push_back(createShipFromInputLine(m_playerId));
    }

    m_dropoffs.clear();
    m_dropoffs.reserve(num_dropoffs);
    for (; num_dropoffs > 0; --num_dropoffs) {
        m_dropoffs.push_back(createDropoffFromInputLine(m_playerId));
    }
}

}  // namespace hlt
