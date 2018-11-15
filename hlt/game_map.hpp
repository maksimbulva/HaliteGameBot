#pragma once

#include "map_cell.hpp"
#include "types.hpp"

#include <vector>

namespace hlt {

class GameMap {
public:
    GameMap(const fastint width, const fastint height);
    ~GameMap();

    const fastint width() const { return m_width; }
    const fastint height() const { return m_height; }

    MapCell* at(const Position& position) {
        Position normalized = normalize(position);
        return &m_cells[toIndex(normalized)];
    }

    MapCell* at(const Entity& entity) {
        return at(entity.position());
    }

    MapCell* at(const Entity* entity) {
        return at(entity->position());
    }

    MapCell* at(const std::shared_ptr<Entity>& entity) {
        return at(entity->position());
    }

    int calculate_distance(const Position& source, const Position& target) {
        const auto& normalized_source = normalize(source);
        const auto& normalized_target = normalize(target);

        const int dx = std::abs(normalized_source.x - normalized_target.x);
        const int dy = std::abs(normalized_source.y - normalized_target.y);

        const int toroidal_dx = std::min(dx, (int)m_width - dx);
        const int toroidal_dy = std::min(dy, (int)m_height - dy);

        return toroidal_dx + toroidal_dy;
    }

    Position normalize(const Position& position) {
        const int x = ((position.x % m_width) + m_width) % m_width;
        const int y = ((position.y % m_height) + m_height) % m_height;
        return { x, y };
    }

    std::vector<Direction> get_unsafe_moves(const Position& source, const Position& destination) {
        const auto& normalized_source = normalize(source);
        const auto& normalized_destination = normalize(destination);

        const int dx = std::abs(normalized_source.x - normalized_destination.x);
        const int dy = std::abs(normalized_source.y - normalized_destination.y);
        const int wrapped_dx = m_width - dx;
        const int wrapped_dy = m_height - dy;

        std::vector<Direction> possible_moves;

        if (normalized_source.x < normalized_destination.x) {
            possible_moves.push_back(dx > wrapped_dx ? Direction::WEST : Direction::EAST);
        } else if (normalized_source.x > normalized_destination.x) {
            possible_moves.push_back(dx < wrapped_dx ? Direction::WEST : Direction::EAST);
        }

        if (normalized_source.y < normalized_destination.y) {
            possible_moves.push_back(dy > wrapped_dy ? Direction::NORTH : Direction::SOUTH);
        } else if (normalized_source.y > normalized_destination.y) {
            possible_moves.push_back(dy < wrapped_dy ? Direction::NORTH : Direction::SOUTH);
        }

        return possible_moves;
    }

/*    Direction naive_navigate(const Ship& ship, const Position& destination) {
        // get_unsafe_moves normalizes for us
        for (auto direction : get_unsafe_moves(ship.position(), destination)) {
            Position target_pos = ship.position().directional_offset(direction);
            if (!at(target_pos)->is_occupied()) {
                at(target_pos)->mark_unsafe(ship);
                return direction;
            }
        }

        return Direction::STILL;
    }
*/

    inline const size_t toIndex(Position pos) const {
        return static_cast<size_t>(pos.x + pos.y * m_width);
    }

    inline const size_t toIndex(const fastint x, const fastint y) const {
        return static_cast<size_t>(x + y * m_width);
    }

    void _update();
    static std::unique_ptr<GameMap> _generate();

private:
    fastint m_width;
    fastint m_height;
    std::vector<MapCell> m_cells;
};

}
