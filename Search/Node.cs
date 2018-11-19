using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaliteGameBot.Search
{
    class Node
    {
        public Node Parent { get; }
        public GameAction GameAction { get; }
        public int Depth { get; }

        public double Evaluation;
        public double Priority;

        public List<Node> Children { get; set; }

        public Node(Node parent, GameAction gameAction, int depth)
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
