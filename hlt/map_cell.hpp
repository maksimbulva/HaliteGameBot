#pragma once

#include "types.hpp"
#include "position.hpp"
#include "ship.hpp"
#include "dropoff.hpp"

namespace hlt {
    struct MapCell {
        // Position position;
        Halite halite;
        // std::shared_ptr<Ship> ship;
        // std::shared_ptr<Entity> structure; // only has dropoffs and shipyards; if id is -1, then it's a shipyard, otherwise it's a dropoff

        MapCell() { }

        MapCell(int x, int y, Halite halite) :
            // position(x, y),
            halite(halite)
        {}

        bool is_empty() const {
            // TODO
            return true;
            // return !ship && !structure;
        }

        bool is_occupied() const {
            // TODO
            return false;
            // return static_cast<bool>(ship);
        }

        void mark_unsafe(const Ship& ship) {
            // TODO
            // this->ship = ship;
        }
    };
}
