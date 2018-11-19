using System.Collections.Generic;

namespace HaliteGameBot.Search
{
    class NodeByPriorityComparator : IComparer<Node>
    {
        public static NodeByPriorityComparator Instance { get; } = new NodeByPriorityComparator();

        public int Compare(Node x, Node y)
        {
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
