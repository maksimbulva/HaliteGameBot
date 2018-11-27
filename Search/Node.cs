using System.Collections.Generic;

namespace HaliteGameBot.Search
{
    internal sealed class Node
    {
        public Node Parent { get; private set; }
        public GameAction GameAction { get; private set; }
        public int Depth { get; private set; }

        public double Evaluation;

        public List<Node> Children { get; private set; }

        public Node BestChild { get; private set; }

        public bool IsRoot => Depth == Tree.ROOT_DEPTH;

        public Node(Node parent, GameAction gameAction, int depth, double evaluation)
        {
            Parent = parent;
            GameAction = gameAction;
            Depth = depth;
            Evaluation = evaluation;
        }

        public void Reuse(Node parent, GameAction gameAction, int depth)
        {
            Parent = parent;
            GameAction = gameAction;
            Depth = depth;
            Evaluation = 0.0;
            ClearChildren();
        }

        public void ClearChildren()
        {
            Children?.Clear();
            Children = null;
            BestChild = null;
        }

        public Node AddChild(GameAction gameAction, double evaluation)
        {
            if (Children == null)
            {
                // TODO - use memory pool
                Children = new List<Node>(5);
            }
            Node child = new Node(this, gameAction, Depth + 1, evaluation);
            Children.Add(child);
            return child;
        }

        public void RemoveChild(Node node)
        {
            for (int i = 0; i < Children.Count; ++i)
            {
                if (ReferenceEquals(node, Children[i]))
                {
                    Children[i] = Children[Children.Count - 1];
                    Children.RemoveAt(Children.Count - 1);
                }
            }
            if (ReferenceEquals(node, BestChild))
            {
                if (Children.Count > 0)
                {
                    BestChild = FindBestChild();
                    Evaluation = BestChild.Evaluation;
                }
                else
                {
                    BestChild = null;
                    Evaluation = double.MinValue;
                }
            }
        }

        public bool EvaluateFromChildren()
        {
            if (Children != null && Children.Count > 0)
            {
                BestChild = FindBestChild();
                Evaluation = BestChild.Evaluation;
                return true;
            }
            return false;
        }

        // Returns true if evaluation of the current node has changed
        public bool OnChildEvaluated(Node child)
        {
            if (Evaluation > child.Evaluation)
            {
                // This is not the best child
                return false;
            }
            if (ReferenceEquals(BestChild, child) && child.Evaluation < BestChild.Evaluation)
            {
                // Best child evaluation has changed, and it became lower
                // Possibly the child is not the best child anymore
                BestChild = FindBestChild();
                Evaluation = BestChild.Evaluation;
            }
            else
            {
                BestChild = child;
                Evaluation = BestChild.Evaluation;
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
                Node curChild = Children[i];
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
