using HaliteGameBot.Search.GameActions;
using System.Collections.Generic;

namespace HaliteGameBot.Search
{
    internal sealed class Node
    {
        public Node Parent { get; }
        public IGameAction GameAction { get; }
        public int Depth { get; }

        public double Evaluation;
        public double Priority;

        public List<Node> Children { get; set; }

        public Node(Node parent, IGameAction gameAction, int depth)
        {
            Parent = parent;
            GameAction = gameAction;
            Depth = depth;
        }

        public void GetParents(List<Node> buffer)
        {
            buffer.Clear();
            for (Node curNode = this; curNode != null; curNode = curNode.Parent)
            {
                buffer.Add(curNode);
            }
        }
    }
}
