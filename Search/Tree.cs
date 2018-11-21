using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    class Tree
    {
        private const int ROOT_DEPTH = 0;

        private readonly Game _game;

        public Node Root { get; }

        public Tree(Game game)
        {
            _game = game;
            Root = new Node(null, null, ROOT_DEPTH);
        }

        public void Clear()
        {
            // TODO: consider using memory pooling
            if (Root.Children != null)
            {
                Root.Children.Clear();
                Root.Children = null;
            }
        }

        public void PropagadeEvaluationChange(Node sourse)
        {
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
