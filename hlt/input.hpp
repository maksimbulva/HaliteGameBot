#pragma once

#include "log.hpp"

#include <string>

#include <iostream>
#include <sstream>

namespace hlt {

std::string_view readToken(const std::string& str, size_t& pos);

    static std::string get_string() {
        std::string result;
        std::getline(std::cin, result);
        if (!std::cin.good()) {
            hlt::log::log("Input connection from server closed. Exiting...");
            exit(0);
        }
        return result;
    }

    static std::stringstream get_sstream() {
        return std::stringstream(get_string());
    }
}
