#include "game.hpp"

#include "factory.hpp"
#include "input.hpp"

#include <sstream>
#include <stdio.h>

namespace hlt {

std::ostream& operator<<(std::ostream& stream, const Command& command) {
    switch (command.type()) {
        case Command::SPAWN_SHIP:
            stream << 'g';
            break;
        case Command::BUILD_DROPOFF_SITE:
            stream << "c " << command.entityId();
        case Command::MOVE:
            stream << "m " << command.entityId() << " " << static_cast<char>(command.direction());
            break;
        default:
            log::log("Do not know how to output command of type " + std::to_string(command.type()));
            exit(1);
    }
    return stream;
}

void Game::readPlayers() {
    int num_players;
    scanf("%d %d", &num_players, &my_id);

    log::open(my_id);

    players.reserve(num_players);
    for (; num_players > 0; --num_players) {
        players.push_back(createPlayerFromInput());
    }

    m_myPlayer = &players[my_id];
}

}  // namespace hlt

hlt::Game::Game()
    :
    turn_number(0),
    m_myPlayer(nullptr)
{
    std::ios_base::sync_with_stdio(false);

    Constants::get().init(hlt::get_string());
    readPlayers();

    game_map = GameMap::_generate();
}

void hlt::Game::ready(const std::string& name) {
    std::cout << name << std::endl;
}

void hlt::Game::update_frame() {
    hlt::get_sstream() >> turn_number;
    log::log("=============== TURN " + std::to_string(turn_number) + " ================");

    for (size_t i = 0; i < players.size(); ++i) {
        PlayerId current_player_id;
        int num_ships;
        int num_dropoffs;
        Halite halite;
        hlt::get_sstream() >> current_player_id >> num_ships >> num_dropoffs >> halite;

        players[current_player_id]._update(num_ships, num_dropoffs, halite);
    }

    game_map->_update();

    for (const auto& player : players) {
        for (const auto& ship : player.ships()) {
            game_map->at(ship)->mark_unsafe(ship);
        }

        // TODO
/*        const Shipyard& shipyard = player.shipyard();
        game_map->at(shipyard.position())->structure =
            std::make_shared<Shipyard>(shipyard.playerId(), shipyard.position());
*/

        for (const auto& dropoff : player.dropoffs()) {
            // TODO
            // game_map->at(dropoff)->structure = dropoff;
        }
    }
}

bool hlt::Game::end_turn(const std::vector<hlt::Command>& commands) {
    for (const auto& command : commands) {
        std::cout << command << ' ';
    }
    std::cout << std::endl;
    return std::cout.good();
}
