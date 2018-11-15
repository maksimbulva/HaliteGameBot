#pragma once

#include "entity.hpp"
#include "command.hpp"

namespace hlt {

class Shipyard : public Entity {
public:
    inline Shipyard(const PlayerId playerId, const Position position)
        :
        Entity(playerId, -1, position)
    {
    }
};

}
