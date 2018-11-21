using System;
using System.Collections.Generic;

using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;
using HaliteGameBot.Search;
using HaliteGameBot.Search.GameActions;

namespace HaliteGameBot
{
    class MyBot
    {
        private readonly Game _game;
        private readonly Strategy _strategy;

        private readonly List<Search.Search> _searches = new List<Search.Search>(16);

        public MyBot(Game game)
        {
            _game = game;
            _strategy = new Strategy();
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

            // TODO: run all searches in parallel
            List<Ship> ships = myPlayer.Ships;
            while (_searches.Count < ships.Count)
            {
                _searches.Add(CreateSearch());
            }

            for (int i = 0; i < ships.Count; ++i)
            {
                Ship ship = ships[i];
                Search.Search search = _searches[i];
                search.Run(ship);
                IGameAction action = search.GetBestAction();
                commands.Add(CreateCommand(ship.EntityId, action));
            }

            if (_game.TurnNumber <= 200
                && myPlayer.Halite >= Constants.ShipCost
                && !gameMap.Occupied[gameMap.GetIndex(myPlayer.Shipyard.Position)])
            {
                commands.Add(Factory.CreateSpawnShipCommand());
            }

            return commands;
        }

        public void OnMoveCompleted()
        {
            _searches.ForEach(search => LogSearchStats(search.Stats));
            _searches.ForEach(search => search.Clear());
        }

        private Search.Search CreateSearch() => new Search.Search(
            game: _game,
            strategy: _strategy,
            queueCapacity: SearchSettings.QUEUE_CAPACITY);

        private ICommand CreateCommand(int entityId, IGameAction action)
        {
            if (action == null || action is StayStill)
            {
                return new Move(entityId, Direction.STAY_STILL);
            }
            throw new NotImplementedException();
        }

        private static void LogSearchStats(ISearchStats stats)
        {
            Log.Write($"[{stats.ThreadId}] {stats.Duration.Milliseconds}ms {stats.ActionCount} actions, {stats.NodeCount} nodes");
        }
    }
} 
