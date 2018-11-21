using System;

namespace HaliteGameBot.Search
{
    interface ISearchStats
    {
        int ThreadId { get; }
        long ActionCount { get; }
        long NodeCount { get; }
        DateTime StartTime { get; }
        DateTime FinishTime { get; }
        TimeSpan Duration { get; }
    }
}
