using System;
using System.Collections.Generic;
using HaliteGameBot.Framework;
using HaliteGameBot.Search.GameActions;

namespace HaliteGameBot.Search
{
    internal sealed class Search
    {
        private readonly GameMapState _gameMapState;

        private readonly Strategy _strategy;

        private readonly Tree _tree = new Tree();
        private readonly PriorityQueue _priorityQueue;

        private readonly List<Node> _parentsBuffer = new List<Node>(32);

        private SearchStats _stats;
        public ISearchStats Stats { get => _stats; }

        public SearchResults Results { get; private set; }

        public double BestEvaluation => _tree.Root.Evaluation;

        public bool IsEmpty { get => _tree.Root == null; }

        public Search(Game game, Strategy strategy, int queueCapacity)
        {
            _gameMapState = new GameMapState(game);

            _strategy = strategy;
            _priorityQueue = new PriorityQueue(queueCapacity);
        }

        public void Clear()
        {
            _tree.Clear();
            _priorityQueue.Clear();
            Results = null;
        }

        public void UpdateMap(IEnumerable<GameMapUpdate> mapUpdates, HashSet<int> dropoffs)
        {
            _gameMapState.ApplyUpdates(mapUpdates);
            _gameMapState.Dropoffs = dropoffs;
        }

        public void Run(Game game, Ship ship)
        {
            if (!_priorityQueue.IsEmpty)
            {
                throw new Exception("Call Clear() before calling new Run()");
            }

            _stats = new SearchStats();
            _priorityQueue.Enqueue(_tree.Root);

            // TODO - for the moment, limit the number of nodes to 100
            for (int i = 0; i < 100 && !_priorityQueue.IsEmpty; ++i)
            {
                ProcessNode(_priorityQueue.Dequeue(), ship, game, _strategy);
            }

            _stats.OnSearchFinished();
            Results = new SearchResults(ship, _tree.Root.Children);

            Log.Write("Search run finished");
        }

        private void ProcessNode(Node node, Ship ship, Game game, Strategy strategy)
        {
            GameState gameState = new GameState(new PlayerState(game.MyPlayer.Halite), _gameMapState);
            node.GetParents(_parentsBuffer);

            // Play node actions from root to the given node
            for (int i = _parentsBuffer.Count - 1; i >= 0; --i)
            {
                IGameAction action = _parentsBuffer[i].GameAction;
                if (action != null)
                {
                    gameState.Play(action);
                }
            }

            List<IGameAction> actions = gameState.GenerateActions(ship);
            _stats.ActionCount += actions.Count;

            foreach (IGameAction action in actions)
            {
                gameState.Play(action);

                double evaluation = _strategy.EvaluateStatic(gameState);
                double priority = _strategy.GetPriority(evaluation, node.Depth + 1);

                if (_priorityQueue.WillEnqueue(priority))
                {
                    ++_stats.NodeCount;
                    Node child = node.AddChild(action, priority, evaluation);
                    _priorityQueue.Enqueue(child);
                }

                gameState.Undo();
            }

            _tree.SetEvaluation(node, node.BestChildEvaluation);
            gameState.UndoAll();
        }
    }
}
