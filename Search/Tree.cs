namespace HaliteGameBot.Search
{
    internal sealed class Tree
    {
        public const int ROOT_DEPTH = 0;

        public Node Root { get; } = new Node(null, null, ROOT_DEPTH, double.NegativeInfinity);

        public void Clear()
        {
            // TODO: consider using memory pooling
            Root.Reuse(null, null, ROOT_DEPTH);
        }

        public void EvaluateAndPropagate(Node node)
        {
            if (node.EvaluateFromChildren())
            {
                PropagateEvaluation(node);
            }
            else
            {
                Remove(node);
            }
        }

        private void PropagateEvaluation(Node node)
        {
            while (node.Parent != null && node.Parent.OnChildEvaluated(node))
            {
                node = node.Parent;
            }
        }

        private void Remove(Node node)
        {
            while (node.Parent != null && !ReferenceEquals(node.Parent, Root))
            {
                node.Parent.RemoveChild(node);
                if (node.Parent.BestChild == null)
                {
                    node = node.Parent;
                }
                else
                {
                    break;
                }
            }
        }

        /*
        private void ClearSubtree(Node root)
        {
            for (int i = 0; i < root.Children.Count; ++i)
            {
                Node child = root.Children[i];
                if (child.Children != null && child.Children.Count > 0)
                {
                    ClearSubtree(child);
                }
            }
        }
        */
    }
}
