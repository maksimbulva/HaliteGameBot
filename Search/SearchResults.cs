using System;
using System.Collections.Generic;
using System.Text;
using HaliteGameBot.Framework;
using HaliteGameBot.Search;

internal sealed class SearchResults
{
    private readonly Entity _entity;

    public int EntityId => _entity.EntityId;

    public Position EntityPosition => _entity.Position;

    // Sorted by evaluation descending
    private List<SearchBranchData> _branches = new List<SearchBranchData>();

    public IReadOnlyList<SearchBranchData> Branches => _branches;

    public SearchResults(Entity entity, IEnumerable<Node> rootChildren)
    {
        _entity = entity;

        if (rootChildren == null)
        {
            return;
        }
        foreach (Node child in rootChildren)
        {
            _branches.Add(new SearchBranchData(child));
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
        private readonly Node _rootChild;

        public GameAction GameAction => _rootChild.GameAction;
        public double Evaluation => _rootChild.Evaluation;

        public SearchBranchData(Node rootChild)
        {
            _rootChild = rootChild;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{Evaluation:0.00}    {GameAction}    ");
            Node node = _rootChild;
            while (node != null)
            {
                switch (node.GameAction.ActionType)
                {
                    case GameActions.STAY_STILL:
                        sb.Append('x');
                        break;
                    case GameActions.MOVE_NORTH:
                        sb.Append('n');
                        break;
                    case GameActions.MOVE_SOUTH:
                        sb.Append('s');
                        break;
                    case GameActions.MOVE_WEST:
                        sb.Append('w');
                        break;
                    case GameActions.MOVE_EAST:
                        sb.Append('e');
                        break;
                    default:
                        sb.Append('?');
                        break;
                }
                if (node.BestChild == null)
                {
                    break;
                }
                node = node.BestChild;
            }
            sb.Append($"     Ship ${node.GameAction.Ship.Halite} / Player ${node.GameAction.Player.Halite}");
            return sb.ToString();
        }
    }
}