using System;

namespace HaliteGameBot.Search
{
    // Heap data structure
    // <see>http://www.thelearningpoint.net/computer-science/data-structures-heaps-with-c-program-source-code</see>
    internal sealed class Heap
    {
        private static readonly Node NO_NODE = new Node(null, null, 0, double.NegativeInfinity);

        private readonly Node[] _items = new Node[1024];

        public int Count { get; private set; }

        public double MinEvaluation => Count > 0 ? _items[1].Evaluation : double.NegativeInfinity;

        public Heap()
        {
            _items[0] = NO_NODE;
        }

        public void Clear()
        {
            for (int i = 1; i < Count; ++i)
            {
                _items[i] = null;
            }
            Count = 0;
        }

        public bool WillAdd(double evaluation) => Count + 1 < _items.Length || evaluation > MinEvaluation;

        public bool TryAdd(Node newItem)
        {
            if (Count + 1 >= _items.Length)
            {
                if (newItem.Evaluation < MinEvaluation)
                {
                    return false;
                }
                else
                {
                    DeleteMin();
                }
            }

            double evaluation = newItem.Evaluation;

            ++Count;
            _items[Count] = newItem;

            int i = Count;
            while (_items[i >> 1].Evaluation > evaluation)
            {
                _items[i] = _items[i >> 1];
                i >>= 1;
            }

            _items[i] = newItem;
            return true;
        }

        public Node DeleteMin()
        {
            if (Count == 0)
            {
                return null;
            }

            Node minItem = _items[1];
            Node lastItem = _items[Count];
            _items[Count] = null;
            --Count;

            double lastItemEvaluation = lastItem.Evaluation;

            int i = 1;
            while (i << 1 <= Count)
            {
                int iNext = i << 1;
                if (iNext != Count && _items[iNext + 1].Evaluation < _items[iNext].Evaluation)
                {
                    ++iNext;
                }
                if (lastItemEvaluation > _items[iNext].Evaluation)
                {
                    _items[i] = _items[iNext];
                    i = iNext;
                }
                else
                {
                    break;
                }
            }

            _items[i] = lastItem;
            return minItem;
        }

        public void ForEach(Action<Node> action)
        {
            for (int i = 1; i < Count; ++i)
            {
                action(_items[i]);
            }
        }
    }
}
