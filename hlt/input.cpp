#include "pch.h"

#include "input.hpp"

namespace hlt {

std::string readToken(const std::string& str, size_t& pos) {
    const size_t maxPos = str.size();
    // Skip whitespaces
    for (; pos < maxPos && str[pos] == ' '; ++pos) {
    }
    // Read token
    const size_t tokenBegin = pos;
    for (; pos < maxPos && str[pos] != ' '; ++pos) {
    }
    return std::string(str.c_str() + tokenBegin, pos - tokenBegin);
}

}
