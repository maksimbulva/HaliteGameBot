using System;
using System.Collections.Generic;

using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;

namespace HaliteGameBot
{
    class MyBot
    {
        private readonly Game _game;
        private readonly Random _random = new Random();

        public MyBot(Game game)
        {
            _game = game;
        }

        public void Initialize()
        {
            // Do precalculations here. We have up to 2 seconds
        }

        public List<ICommand> GenerateTurnCommands()
        {
            var commands = new List<ICommand>();

            Player myPlayer = _game.MyPlayer;
            GameMap gameMap = _game.GameMap;

            foreach (Ship ship in myPlayer.Ships)
            {
                if (ship.IsFull || gameMap.GetHaliteAt(ship) < Constants.MaxHalite / 10)
                {
                    var randomDirection = DirectionUseCase.RandomCardinalDirection(_random);
                    commands.Add(Factory.CreateMoveCommand(ship, randomDirection));
                }
                else
                {
                    commands.Add(Factory.CreateMoveCommand(ship, Direction.STAY_STILL));
                }
            }

            if (_game.TurnNumber <= 200
                && myPlayer.Halite >= Constants.ShipCost
                /*&& !game_map.at(me.shipyard())->is_occupied()*/)
            {
                commands.Add(Factory.CreateSpawnShipCommand());
            }

            return commands;
        }
    }
}
