#pragma once

#include "types.hpp"
#include "shipyard.hpp"
#include "ship.hpp"
#include "dropoff.hpp"

#include <memory>
#include <unordered_map>

namespace hlt {
    struct Player {
    public:
        PlayerId id;
        Halite halite;
        std::unordered_map<EntityId, std::shared_ptr<Ship>> ships;
        std::unordered_map<EntityId, std::shared_ptr<Dropoff>> dropoffs;

        Player(PlayerId player_id, int shipyard_x, int shipyard_y) :
            id(player_id),
            halite(0),
            m_shipyard(player_id, Position(shipyard_x, shipyard_y))
        {}

        const Shipyard& shipyard() const { return m_shipyard; }

        void _update(int num_ships, int num_dropoffs, Halite halite);
        static std::shared_ptr<Player> _generate();

    private:
        const Shipyard m_shipyard;
    };
}
