#include "factory.hpp"

#include "input.hpp"

#include <stdio.h>

namespace hlt {

Dropoff createDropoffFromInputLine(const PlayerId playerId) {
    int entityId;
    int x;
    int y;
    scanf("%d %d %d", &entityId, &x, &y);
    readUntilEol();
    return Dropoff(playerId, entityId, Position(x, y));
}

Player createPlayerFromInput() {
    int playerId;
    int shipyardX;
    int shipyardY;
    scanf("%d %d %d", &playerId, &shipyardX, &shipyardY);
    readUntilEol();
    return Player(playerId, Position(shipyardX, shipyardY));
}

Ship createShipFromInputLine(const PlayerId playerId) {
    int entityId;
    int x;
    int y;
    int halite;
    scanf("%d %d %d %d", &entityId, &x, &y, &halite);
    readUntilEol();
    return Ship(playerId, entityId, Position(x, y), halite);
}

}  // namespace hlt
