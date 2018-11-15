#pragma once

#include "types.hpp"
#include "position.hpp"

namespace hlt {

class Entity {
public:
    const PlayerId playerId() const { return m_playerId; }
    const EntityId entityId() const { return m_entityId; }
    const Position& position() const { return m_position; }

protected:
    inline Entity(const PlayerId playerId, const EntityId entityId, const Position position)
        :
        m_playerId(playerId),
        m_entityId(entityId),
        m_position(position)
    {
    }

protected:
    const PlayerId m_playerId;
    const EntityId m_entityId; // if id is -1, then it's a shipyard
    const Position m_position;
};

}
