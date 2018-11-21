using System.Collections.Generic;
using HaliteGameBot.Framework;
using HaliteGameBot.Search.GameActions;

namespace HaliteGameBot.Search
{
    class Search
    {
        private readonly Game _game;
        private readonly Strategy _strategy;
        private readonly Tree _tree;
        private readonly PriorityQueue _priorityQueue;

        private readonly List<Node> _parentsBuffer = new List<Node>(32);

        private SearchStats _stats;
        public ISearchStats Stats { get => _stats; }

        public Search(Game game, Strategy strategy, int queueCapacity)
        {
            _game = game;
            _strategy = strategy;
            _tree = new Tree(game);
            _priorityQueue = new PriorityQueue(queueCapacity);
        }

        public void Clear()
        {
            _tree.Clear();
            while (!_priorityQueue.IsEmpty)
            {
                _priorityQueue.Dequeue();
            }
        }

        public void Run(Ship ship)
        {
            _stats = new SearchStats();

            Evaluate(new GameState(_game), _tree.Root);
            _priorityQueue.Enqueue(_tree.Root);

            // TODO - for the moment, limit the number of nodes to 100
            for (int i = 0; i < 100; ++i)
            {
                ProcessNode(_priorityQueue.Dequeue(), ship, _strategy);
            }

            _stats.OnSearchFinished();
        }

        public IGameAction GetBestAction()
        {
            Node bestChild = null;
            _tree.Root?.Children.ForEach(node =>
            {
                if (bestChild == null || node.Evaluation > bestChild.Evaluation)
                {
                    bestChild = node;
                }
            });
            return bestChild.GameAction;
        }

        private void ProcessNode(Node node, Ship ship, Strategy strategy)
        {
            if (node == null)
            {
                return;
            }

            GameState gameState = new GameState(_game);
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

            int childDepth = node.Depth + 1;
            List<Node> children = null;

            double maxChildrenEvaluation = double.MinValue;
            List<IGameAction> actions = gameState.GenerateActions(ship);
            _stats.ActionCount += actions.Count;

            foreach (IGameAction action in actions)
            {
                gameState.Play(action);

                double evaluation = _strategy.EvaluateStatic(gameState);
                if (evaluation > maxChildrenEvaluation)
                {
                    maxChildrenEvaluation = evaluation;
                }

                double priority = _strategy.GetPriority(evaluation, childDepth);
                if (_priorityQueue.WillEnqueue(priority))
                {
                    Node child = new Node(node, action, childDepth);
                    ++_stats.NodeCount;
                    if (children == null)
                    {
                        children = new List<Node>(5);
                    }
                    children.Add(child);
                    _priorityQueue.Enqueue(child);
                }

                gameState.Undo();
            }

            node.Children = children;

            if (maxChildrenEvaluation > node.Evaluation)
            {
                node.Evaluation = maxChildrenEvaluation;
                _tree.PropagadeEvaluationChange(node);
            }
        }

        private void Evaluate(GameState gameState, Node node)
        {
            node.Evaluation = _strategy.EvaluateStatic(gameState);
            node.Priority = _strategy.GetPriority(node.Evaluation, node.Depth);
        }

        // private double GetPriority(Node node) => _strategy.GetPriority(node.Evaluation, node.Depth);
    }
}
