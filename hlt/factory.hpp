#pragma once

#include "dropoff.hpp"
#include "player.hpp"
#include "ship.hpp"
#include "types.hpp"

namespace hlt {

Dropoff createDropoffFromInputLine(PlayerId playerId);

Player createPlayerFromInput();

Ship createShipFromInputLine(PlayerId playerId);

}