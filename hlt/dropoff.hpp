#pragma once

#include "entity.hpp"
#include "types.hpp"

namespace hlt {

class Dropoff : public Entity {
public:
    Dropoff(const PlayerId playerId, const EntityId entityId, const Position position)
        :
        Entity(playerId, entityId, position)
    {
    }
};

}
