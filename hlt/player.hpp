#pragma once

#include "dropoff.hpp"
#include "ship.hpp"
#include "shipyard.hpp"
#include "types.hpp"

#include <vector>

namespace hlt {
    struct Player {
    public:
        Player(PlayerId player_id, Position shipyard);

        const PlayerId playerId() const { return m_playerId; }
        const Halite halite() const { return m_halite; }
        const Shipyard& shipyard() const { return m_shipyard; }

        const std::vector<Ship>& ships() const { return m_ships; }
        const std::vector<Dropoff>& dropoffs() const { return m_dropoffs; }

        void _update(int num_ships, int num_dropoffs, Halite halite);

    private:
        const PlayerId m_playerId;
        Halite m_halite;
        const Shipyard m_shipyard;

        std::vector<Ship> m_ships;
        std::vector<Dropoff> m_dropoffs;
    };
}
