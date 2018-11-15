cmake_minimum_required(VERSION 2.8)

project(MyBot)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_SOURCE_DIR})

# set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -g -O2 -Wall -Wno-unused-function -Wno-unused-result -pedantic")
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -g -O2 -Wall -pedantic")

include_directories(${CMAKE_SOURCE_DIR}/hlt)

file(GLOB source_files
	"*.cpp"
	"hlt/*.hpp"
	"hlt/*.cpp"
)

add_executable(MyBot ${source_files})
