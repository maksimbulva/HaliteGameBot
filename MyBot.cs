﻿using System.Collections.Generic;

using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;

namespace HaliteGameBot
{
    internal sealed class MyBot
    {
        private readonly Game _game;
        private readonly Strategy _strategy;
        private readonly SearchManager _searchManager;
        private readonly List<ICommand> _commandsBuffer = new List<ICommand>(32);

        public MyBot(Game game)
        {
            _game = game;
            _strategy = new Strategy();
            _searchManager = new SearchManager(_game, _strategy);
        }

        public void Initialize()
        {
            // Do precalculations here. We have up to 2 seconds
        }

        public IEnumerable<ICommand> GenerateTurnCommands()
        {
            Player myPlayer = _game.MyPlayer;
            GameMap gameMap = _game.GameMap;

            Log.Write($"{myPlayer.Ships.Count} ships, {myPlayer.Dropoffs.Count} dropoffs");

            _commandsBuffer.Clear();

            if (_game.TurnNumber <= 200
                && myPlayer.Halite >= Constants.ShipCost
                && !myPlayer.Ships.Exists(ship => ship.Position.Equals(myPlayer.Shipyard.Position)))
            {
                _commandsBuffer.Add(Factory.CreateSpawnShipCommand());
                Log.Write("Spawn new ship");
            }

            _searchManager.OnGameUpdated();
            _searchManager.RunAll();
            _commandsBuffer.AddRange(_searchManager.CollectCommands());

            return _commandsBuffer;
        }

        public void OnMoveCompleted()
        {
            _searchManager.LogStats();
            _searchManager.Clear();
        }
    }
} 
