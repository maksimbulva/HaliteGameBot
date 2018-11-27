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

        private readonly Heap[] _heaps = new Heap[2]
        {
            new Heap(),
            new Heap()
        };

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
        }

        public void Clear()
        {
            _tree.Clear();
            _heaps[0].Clear();
            _heaps[1].Clear();
            Results = null;
        }

        public void UpdateMap(IEnumerable<GameMapUpdate> mapUpdates, HashSet<int> dropoffs)
        {
            _gameMapState.ApplyUpdates(mapUpdates);
            _gameMapState.Dropoffs = dropoffs;
        }

        public void Run(Game game, Ship ship, HashSet<int> bannedCells)
        {
            if (_tree.Root.Children != null || _heaps[0].Count > 0 || Results != null)
            {
                throw new Exception("Call Clear() before calling new Run()");
            }

            _tree.Root.Reuse(null, GameAction.CreateRootAction(game, ship), Tree.ROOT_DEPTH);

            _stats = new SearchStats();

            int curDepthHeapIndex = 0;
            _heaps[curDepthHeapIndex].TryAdd(_tree.Root);

            for (int depth = Tree.ROOT_DEPTH; depth < 2; ++depth)
            {
                Heap curDepthHeap = _heaps[curDepthHeapIndex];
                Heap nextDepthHeap = _heaps[1 - curDepthHeapIndex];
                nextDepthHeap.Clear();

                curDepthHeap.ForEach(node =>
                {
                    ProcessNode(node, nextDepthHeap, depth == Tree.ROOT_DEPTH ? bannedCells : null);
                    _tree.EvaluateAndPropagate(node);
                });

                curDepthHeapIndex = 1 - curDepthHeapIndex;

                if (_tree.Root.Children == null || _tree.Root.Children.Count == 1 || nextDepthHeap.Count == 0)
                {
                    break;
                }
            }

            _stats.OnSearchFinished();
            Results = new SearchResults(ship, _tree.Root.Children);
        }

        private void ProcessNode(Node node, Heap nextDepthHeap, HashSet<int> bannedCells)
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
                if (bannedCells != null && IsBanned(gameAction, bannedCells))
                {
                    continue;
                }

                double evaluation = _strategy.EvaluateStatic(gameState);
                if (nextDepthHeap.WillAdd(evaluation))
                {
                    ++_stats.NodeCount;
                    Node child = node.AddChild(gameAction, evaluation);
                    nextDepthHeap.TryAdd(child);
                }

                gameState.Undo();
            }

            gameState.UndoAll();
        }

        private bool IsBanned(GameAction gameAction, HashSet<int> bannedCells)
        {
            int cellIndex = _gameMapState.GetCellIndex(gameAction.Ship.X, gameAction.Ship.Y);
            return bannedCells.Contains(cellIndex);
        }
    }
}
