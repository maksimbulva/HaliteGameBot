﻿using System;

namespace HaliteGameBot.Search
{
    sealed class PriorityQueue
    {
        private readonly C5.IntervalHeap<Node> _priorityQueue;

        public int Capacity { get; }

        public int Count { get; private set; }

        public double MinPriority { get; private set; }

        public bool IsEmpty => Count == 0;

        public PriorityQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("capacity must be a positive number");
            }
            Capacity = capacity;
            Count = 0;
            MinPriority = double.MinValue;

            _priorityQueue = new C5.IntervalHeap<Node>(
                Capacity,
                NodeByPriorityComparator.Instance,
                C5.MemoryType.Strict
           );
        }

        public Node Dequeue()
        {
            if (IsEmpty)
            {
                return null;
            }
            else
            {
                Node result = _priorityQueue.DeleteMax();
                --Count;
                if (IsEmpty)
                {
                    MinPriority = double.MinValue;
                }
                return result;
            }
        }

        public bool Enqueue(Node node)
        {
            bool wasAdded = false;
            if (Count == Capacity)
            {
                if (MinPriority < node.Priority)
                {
                    _priorityQueue.DeleteMin();
                    _priorityQueue.Add(node);
                    MinPriority = _priorityQueue.FindMin().Priority;
                    wasAdded = true;
                }
            }
            else
            {
                _priorityQueue.Add(node);
                ++Count;
                MinPriority = Math.Min(MinPriority, node.Priority);
                wasAdded = true;
            }
            return wasAdded;
        }

        public bool WillEnqueue(double priority) => Count < Capacity || priority > MinPriority;
    }
}