using System.Collections.Generic;
using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    class Search
    {
        private readonly Game _game;
        private readonly Strategy _strategy;
        private readonly Tree _tree;
        private readonly PriorityQueue _priorityQueue;

        private readonly List<Node> _parentsBuffer = new List<Node>(32);

        public bool HasNodeToProcess { get { return !_priorityQueue.IsEmpty; } }

        public Search(Game game, Strategy strategy, int maxQueueSize)
        {
            _strategy = strategy;
            _tree = new Tree(game);
            _priorityQueue = new PriorityQueue(maxQueueSize);


            Evaluate(new GameState(game), _tree.Root);
            _priorityQueue.Enqueue(_tree.Root);
        }

        public void Run()
        {
            // TODO - for the moment, limit the number of nodes to 100
            for (int i = 0; i < 100; ++i)
            {
                ProcessNode(_priorityQueue.Dequeue(), _strategy);
            }
        }

        private void ProcessNode(Node node, Strategy strategy)
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
                gameState.Play(_parentsBuffer[i].GameAction);
            }

            int childDepth = node.Depth + 1;
            List<Node> children = null;

            double maxChildrenEvaluation = double.MinValue;

            foreach (GameAction action in gameState.GenerateActions())
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
