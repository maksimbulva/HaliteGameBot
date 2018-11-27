using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;
using HaliteGameBot.Search;
using System.Collections.Generic;
using System.Linq;

namespace HaliteGameBot
{
    internal sealed class SearchManager
    {
        private readonly Game _game;
        private readonly GameMap _gameMap;
        private readonly Strategy _strategy;

        private readonly List<Search.Search> _searches = new List<Search.Search>(32);

        private readonly HashSet<int> _blockedCells = new HashSet<int>(32);
        private readonly HashSet<int> _dropoffCells = new HashSet<int>(32);
        private readonly List<ICommand> _commandsBuffer = new List<ICommand>(32);

        public SearchManager(Game game, Strategy strategy)
        {
            _game = game;
            _gameMap = _game.GameMap;
            _strategy = strategy;
        }

        public void OnGameUpdated()
        {
            UpdateDropoffs();
            while (_searches.Count < _game.MyPlayer.Ships.Count)
            {
                _searches.Add(CreateSearch());
            }
            _searches.ForEach(search => search.UpdateMap(_game.MapUpdates, _dropoffCells));
        }

        public void RunAll(HashSet<int> bannedCells)
        {
            // TODO: run all searches in parallel
            var myShips = _game.MyPlayer.Ships;
            for (int i = 0; i < myShips.Count; ++i)
            {
                _searches[i].Run(_game, myShips[i], bannedCells);
            }
        }

        public IEnumerable<ICommand> CollectCommands()
        {
            _commandsBuffer.Clear();

            var myShips = _game.MyPlayer.Ships;
            // Sort first so that ships with low evaluation do not block movement for other ships
            var sortedIndices = Enumerable.Range(0, myShips.Count).ToList();
            sortedIndices.Sort((i1, i2) =>
                -_searches[i1].BestEvaluation.CompareTo(_searches[i2].BestEvaluation));

            _blockedCells.Clear();
            foreach (int i in sortedIndices)
            {
                Search.Search search = _searches[sortedIndices[i]];
                foreach (var branchData in search.Results.Branches)
                {
                    GameAction gameAction = branchData.GameAction;
                    int cellIndex = _gameMap.GetIndex(gameAction.Ship.X, gameAction.Ship.Y);
                    if (!_blockedCells.Contains(cellIndex))
                    {
                        _commandsBuffer.Add(CommandFactory.FromGameAction(
                            gameAction.ActionType,
                            search.Results.EntityId));
                        _blockedCells.Add(cellIndex);
                        break;
                    }
                }
            }

            return _commandsBuffer;
        }

        public void Clear() => _searches.ForEach(search => search.Clear());

        public void LogStats()
        {
            _searches.Where(it => !it.IsEmpty).ToList().ForEach(it =>
            {
                Log.Write(it.Stats.ToString());
                Log.Write(it.Results.ToString());
            });
        }

        private Search.Search CreateSearch() => new Search.Search(
            game: _game,
            strategy: _strategy,
            queueCapacity: SearchSettings.QUEUE_CAPACITY);

        private void UpdateDropoffs()
        {
            _dropoffCells.Clear();
            foreach (Dropoff dropoff in _game.MyPlayer.Dropoffs)
            {
                _dropoffCells.Add(_gameMap.GetIndex(dropoff.Position));
            }
            _dropoffCells.Add(_gameMap.GetIndex(_game.MyPlayer.Shipyard.Position));
        }
    }
}
