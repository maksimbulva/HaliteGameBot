using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    class Tree
    {
        private readonly Game _game;

        public Node Root { get; }

        public Tree(Game game)
        {
            _game = game;
            Root = new Node(null, null, 0);
        }

        public void PropagadeEvaluationChange(Node sourse)
        {
        }
    }
}
