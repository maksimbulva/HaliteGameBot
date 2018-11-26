using System;
using System.Collections.Generic;
using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    internal sealed class Search
    {
        private readonly GameMapState _gameMapState;

        private readonly Strategy _strategy;

        private readonly Tree _tree = new Tree();
        private readonly PriorityQueue _priorityQueue;

        private readonly Stack<Node> _parentsBuffer = new Stack<Node>(32);

        private SearchStats _stats;
        public ISearchStats Stats => _stats;

        public SearchResults Results { get; private set; }

        public double BestEvaluation => _tree.Root.Evaluation;

        public bool IsEmpty => _tree.Root.Children == null || _tree.Root.Children.Count == 0;

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

        public void Run(Game game, Ship ship, HashSet<int> bannedCells)
        {
            if (!_priorityQueue.IsEmpty)
            {
                throw new Exception("Call Clear() before calling new Run()");
            }

            _tree.Root.Reuse(null, GameAction.CreateRootAction(game, ship), Tree.ROOT_DEPTH);

            _stats = new SearchStats();
            _priorityQueue.Enqueue(_tree.Root);

            // TODO - for the moment, limit the number of nodes to 100
            for (int i = 0; i < 100 && !_priorityQueue.IsEmpty; ++i)
            {
                ProcessNode(_priorityQueue.Dequeue(), ship, game, _strategy, bannedCells);
            }

            _stats.OnSearchFinished();
            Results = new SearchResults(ship, _tree.Root.Children);
        }

        private void ProcessNode(Node node, Ship ship, Game game, Strategy strategy, HashSet<int> bannedCells)
        {
            // TODO - creating a new object of this type is expensive
            GameState gameState = new GameState(_gameMapState);

            // Play node actions from root to the given node
            node.FillWithParents(_parentsBuffer);
            while (_parentsBuffer.Count > 0)
            {
                gameState.Play(_parentsBuffer.Pop().GameAction);
            }

            foreach (GameAction gameAction in gameState.GenerateChildrenActions(node.GameAction))
            {
                ++_stats.ActionCount;

                gameState.Play(gameAction);

                double evaluation = _strategy.EvaluateStatic(gameState);
                double priority = _strategy.GetPriority(evaluation, node.Depth + 1);

                if (_priorityQueue.WillEnqueue(priority)
                    && !(node.IsRoot && IsBanned(gameAction, bannedCells)))
                {
                    ++_stats.NodeCount;
                    Node child = node.AddChild(gameAction, priority, evaluation);
                    _priorityQueue.Enqueue(child);
                }

                gameState.Undo();
            }

            _tree.SetEvaluation(node, node.BestChildEvaluation);
            gameState.UndoAll();
        }

        private bool IsBanned(GameAction gameAction, HashSet<int> bannedCells)
        {
            if (bannedCells != null)
            {
                int cellIndex = _gameMapState.GetCellIndex(gameAction.Ship.X, gameAction.Ship.Y);
                return bannedCells.Contains(cellIndex);
            }
            return false;
        }
    }
}
