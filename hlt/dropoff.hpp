#pragma once

#include "entity.hpp"

#include <memory>

namespace hlt {

class Dropoff : public Entity {
public:
    inline Dropoff(const PlayerId playerId, const EntityId entityId, const Position position)
        :
        Entity(playerId, entityId, position)
    {
    }

    static std::shared_ptr<Dropoff> _generate(PlayerId player_id);
};

}
