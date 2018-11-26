using System.Collections.Generic;

namespace HaliteGameBot.Search
{
    internal sealed class Node
    {
        public Node Parent { get; private set; }
        public GameAction GameAction { get; private set; }
        public int Depth { get; private set; }

        public double Evaluation;
        public double Priority;

        public List<Node> Children { get; private set; }

        public Node BestChild { get; private set; }

        public double BestChildEvaluation { get; private set; } = double.MinValue;

        public bool IsRoot => Depth == Tree.ROOT_DEPTH;

        public Node(Node parent, GameAction gameAction, int depth)
        {
            Parent = parent;
            GameAction = gameAction;
            Depth = depth;
        }

        public void Reuse(Node parent, GameAction gameAction, int depth)
        {
            Parent = parent;
            GameAction = gameAction;
            Depth = depth;
            Evaluation = 0.0;
            Priority = 0.0;
            ClearChildren();
        }

        public void ClearChildren()
        {
            Children?.Clear();
            Children = null;
            BestChild = null;
            BestChildEvaluation = double.MinValue;
        }

        public Node AddChild(GameAction gameAction, double priority, double evaluation)
        {
            if (Children == null)
            {
                // TODO - use memory pool
                Children = new List<Node>(6);
            }
            Node child = new Node(this, gameAction, Depth + 1)
            {
                Priority = priority,
                Evaluation = evaluation
            };
            Children.Add(child);
            OnChildEvaluated(child, evaluation);
            return child;
        }

        // Returns true if evaluation of the current node has changed
        public bool OnChildEvaluated(Node child, double childEvaluation)
        {
            if (BestChild != null && childEvaluation < BestChildEvaluation)
            {
                // This is not the best child
                return false;
            }
            if (BestChild == child && childEvaluation < BestChildEvaluation)
            {
                // Best child evaluation has changed, and it became lower
                // Possibly the child is not the best child anymore
                BestChild = FindBestChild();
                BestChildEvaluation = BestChild.Evaluation;
            }
            else
            {
                BestChild = child;
                BestChildEvaluation = childEvaluation;
            }
            return true;
        }

        public void FillWithParents(Stack<Node> buffer)
        {
            buffer.Clear();
            for (Node curNode = this; curNode != null; curNode = curNode.Parent)
            {
                buffer.Push(curNode);
            }
        }

        private Node FindBestChild()
        {
            Node bestChild = Children[0];
            double bestEvaluation = bestChild.Evaluation;
            for (int i = 1; i < Children.Count; ++i)
            {
                Node curChild = Children[1];
                if (curChild.Evaluation > bestEvaluation)
                {
                    bestChild = curChild;
                    bestEvaluation = curChild.Evaluation;
                }
            }
            return bestChild;
        }
    }
}
