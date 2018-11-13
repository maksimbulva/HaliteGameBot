#pragma once

#include "entity.hpp"
#include "constants.hpp"
#include "command.hpp"

#include <memory>

namespace hlt {
    struct Ship : Entity {
        Halite halite;

        Ship(PlayerId player_id, EntityId ship_id, int x, int y, Halite halite) :
            Entity(player_id, ship_id, x, y),
            halite(halite)
        {}

        bool is_full() const {
            return halite >= Constants::get().maxHalite();
        }

        Command make_dropoff() const {
            return Command::createBuildDropoffSiteCommand(id);
        }

        Command move(Direction direction) const {
            return Command::createMoveCommand(id, direction);
        }

        Command stay_still() const {
            return Command::createMoveCommand(id, Direction::STILL);
        }

        static std::shared_ptr<Ship> _generate(PlayerId player_id);
    };
}
