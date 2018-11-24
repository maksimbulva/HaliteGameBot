using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    internal sealed class Tree
    {
        private const int ROOT_DEPTH = 0;

        private readonly Game _game;

        public Node Root { get; } = new Node(null, null, ROOT_DEPTH);

        public Tree(Game game)
        {
            _game = game;
        }

        public void Clear()
        {
            // TODO: consider using memory pooling
            Root.Reuse(null, null, ROOT_DEPTH);
        }

        public void SetEvaluation(Node node, double evaluation)
        {
            node.Evaluation = evaluation;
            while (node.Parent != null)
            {
                if (!node.Parent.OnChildEvaluated(node, node.Evaluation)) 
                {
                    break;
                }
                node = node.Parent;
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
