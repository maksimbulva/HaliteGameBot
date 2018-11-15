#include "game_map.hpp"
#include "input.hpp"

#include <stdio.h>

namespace hlt {

GameMap::GameMap(const fastint width, const fastint height)
    :
    m_width(width),
    m_height(height),
    m_cells(width * height)
{
}

GameMap::~GameMap() {
}

void GameMap::_update() {
    // TODO
    /*for (auto& cell : m_cells) {
        cell.ship.reset();
    }*/

    int updateCount;
    scanf("%d", &updateCount);

    for (; updateCount > 0; --updateCount) {
        int x;
        int y;
        int halite;
        scanf("%d %d %d", &x, &y, &halite);
        if (x < 0 || x >= m_width || y < 0 || y >= m_height) {
            log::log("Input cell is out of range: x=" + std::to_string(x) + ", y=" + std::to_string(y));
            exit(1);
        }
        const size_t index = toIndex(x, y);
        m_cells[index].halite = halite;
    }

    readUntilEol();
}

std::unique_ptr<GameMap> GameMap::_generate() {
    int width;
    int height;
    scanf("%d %d", &width, &height);

    log::log("Generate map of size width=" + std::to_string(width) + ", height=" + std::to_string(height));

    std::unique_ptr<GameMap> map = std::make_unique<GameMap>(width, height);
    for (auto& cell : map->m_cells) {
        Halite halite;
        scanf("%d", &halite);
        cell.halite = halite;
    }

    readUntilEol();

    return map;
}

}  // namespace hlt

