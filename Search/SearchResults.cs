using System;
using System.Collections.Generic;
using HaliteGameBot.Framework;
using HaliteGameBot.Search;
using HaliteGameBot.Search.GameActions;

internal sealed class SearchResults
{
    private readonly Entity _entity;

    public int EntityId { get => _entity.EntityId; }

    public Position EntityPosition { get => _entity.Position; }

    // Sorted by evaluation descending
    private List<SearchBranchData> _branches = new List<SearchBranchData>();

    public IReadOnlyList<SearchBranchData> Branches { get => _branches; }

    public SearchResults(Entity entity, IEnumerable<Node> rootChildren)
    {
        _entity = entity;

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

    public override string ToString()
    {
        return "Entity id " + EntityId + " at (" + EntityPosition + ")" + Environment.NewLine + "\t"
            + string.Join(Environment.NewLine + "\t", _branches);
    }

    public class SearchBranchData
    {
        public IGameAction GameAction { get; }
        public double Evaluation { get; }

        public SearchBranchData(IGameAction gameAction, double evaluation)
        {
            GameAction = gameAction;
            Evaluation = evaluation;
        }

        public override string ToString() => $"{Evaluation:0.00}    {GameAction}";
    }
}