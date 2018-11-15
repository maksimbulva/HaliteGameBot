#pragma once

#include "command.hpp"
#include "constants.hpp"
#include "entity.hpp"

namespace hlt {

class Ship : public Entity {
public:
    Ship(
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

protected:
    Halite m_halite;
};

}
