using System;
using System.Threading;

namespace HaliteGameBot.Search
{
    sealed class SearchStats : ISearchStats
    {
        public int ThreadId { get; set; }
        public long ActionCount { get; set; }
        public long NodeCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan Duration => FinishTime - StartTime;

        public SearchStats()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            StartTime = DateTime.Now;
            FinishTime = DateTime.MaxValue;
        }

        public void OnSearchFinished() => FinishTime = DateTime.Now;
    }
}
