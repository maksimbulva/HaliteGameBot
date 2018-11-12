#pragma once

#include "direction.hpp"
#include "types.hpp"

#include <string>

namespace hlt {
/*
enum CommandType {
    GENERATE,
    CONSTRUCT,
    MOVE
};

class Command {
public:
    Command(const CommandType type, const EntityId entityId, const Direction direction)
        :
        m_type(type),
        m_entityId(entityId),
        m_direction(direction)
    {
    }

    const CommandType type() const { return m_type; }
    const EntityId entityId() const { return m_entityId; }
    const Direction direction() const { return m_direction; }

    std::string toString() const;

private:
    CommandType m_type;
    EntityId m_entityId;
    Direction m_direction;
};
*/

    typedef std::string Command;

    namespace command {
        Command spawn_ship();
        Command transform_ship_into_dropoff_site(EntityId id);
        Command move(EntityId id, Direction direction);
    }
}
