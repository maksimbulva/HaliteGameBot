#pragma once

#include "entity.hpp"
#include "constants.hpp"
#include "command.hpp"

#include <memory>

namespace hlt {

class Ship : public Entity {
public:
    inline Ship(
        const PlayerId playerId,
        const EntityId entityId,
        const Position position,
        const Halite halite
    )
        :
        Entity(playerId, entityId, position),
        m_halite(halite)
    {
    }

    const bool isFull() const {
        return m_halite >= Constants::get().maxHalite();
    }

    Command make_dropoff() const {
        return Command::createBuildDropoffSiteCommand(m_entityId);
    }

    Command move(Direction direction) const {
        return Command::createMoveCommand(m_entityId, direction);
    }

    Command stay_still() const {
        return Command::createMoveCommand(m_entityId, Direction::STILL);
    }

    static std::shared_ptr<Ship> _generate(PlayerId player_id);

protected:
    Halite m_halite;
};

}
