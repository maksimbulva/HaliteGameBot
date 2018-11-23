using System;

namespace HaliteGameBot.Search
{
    internal interface ISearchStats
    {
        int ThreadId { get; }
        long ActionCount { get; }
        long NodeCount { get; }
        DateTime StartTime { get; }
        DateTime FinishTime { get; }
        TimeSpan Duration { get; }
    }
}
