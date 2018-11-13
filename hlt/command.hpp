#pragma once

#include "direction.hpp"
#include "types.hpp"

#include <string>

namespace hlt {

// Avoid inheritance in attempt to reduce the number of on heap allocations

class Command {
public:
    enum CommandType {
        SPAWN_SHIP,
        BUILD_DROPOFF_SITE,
        MOVE
    };

public:
    static Command createSpawnShipCommand() {
        return Command(SPAWN_SHIP, 0, Direction::DIRECTION_NONE);
    }

    static Command createBuildDropoffSiteCommand(EntityId id) {
        return Command(BUILD_DROPOFF_SITE, id, Direction::DIRECTION_NONE);
    }
    
    static Command createMoveCommand(EntityId id, Direction direction) {
        return Command(MOVE, id, direction);
    }

public:
    const CommandType type() const { return m_type; }
    const EntityId entityId() const { return m_entityId; }
    const Direction direction() const { return m_direction; }

private:
    Command(const CommandType type, const EntityId entityId, const Direction direction)
        :
        m_type(type),
        m_entityId(entityId),
        m_direction(direction)
    {
    }

private:
    CommandType m_type;
    EntityId m_entityId;
    Direction m_direction;
};

}  // namespace hlt
