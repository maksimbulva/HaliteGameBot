using System.Collections.Generic;
using HaliteGameBot.Search;
using HaliteGameBot.Search.GameActions;

internal sealed class SearchResults
{
    // Sorted by evaluation descending
    private List<SearchBranchData> _branches = new List<SearchBranchData>();

    public IReadOnlyList<SearchBranchData> Branches { get => _branches; }

    public SearchResults(IEnumerable<Node> rootChildren)
    {
        if (rootChildren == null)
        {
            return;
        }
        foreach (Node child in rootChildren)
        {
            _branches.Add(new SearchBranchData(child.GameAction, child.Priority));
        }
        _branches.Sort((lhs, rhs) =>
        {
            return -lhs.Evaluation.CompareTo(rhs.Evaluation);
        });
    }

    public struct SearchBranchData
    {
        public IGameAction GameAction { get; }
        public double Evaluation { get; }

        public SearchBranchData(IGameAction gameAction, double evaluation)
        {
            GameAction = gameAction;
            Evaluation = evaluation;
        }
    }
}