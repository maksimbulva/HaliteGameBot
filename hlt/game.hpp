#pragma once

#include "constants.hpp"
#include "game_map.hpp"
#include "player.hpp"
#include "types.hpp"

#include <vector>
#include <iostream>

namespace hlt {
    struct Game {
        Player& myPlayer() { return *m_myPlayer; }

        int turn_number;
        PlayerId my_id;
        std::vector<Player> players;
        std::unique_ptr<GameMap> game_map;

        Game();
        void ready(const std::string& name);
        void update_frame();
        bool end_turn(const std::vector<Command>& commands);

    private:
        void readPlayers();

        Player* m_myPlayer;
    };
}
