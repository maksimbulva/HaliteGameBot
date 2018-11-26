using System.Collections.Generic;
using HaliteGameBot.Framework;

namespace HaliteGameBot.Search
{
    internal sealed class GameState
    {
        private static readonly GameActions[] _moveDirOrder = new GameActions[4]
        {
            GameActions.MOVE_NORTH,
            GameActions.MOVE_WEST,
            GameActions.MOVE_EAST,
            GameActions.MOVE_SOUTH
        };

        private readonly GameMapState _gameMapState;
        private readonly Stack<GameAction> _gameActions = new Stack<GameAction>(32);
        private readonly Stack<GameMapUpdate> _undoStack = new Stack<GameMapUpdate>(32);

        private readonly Position[] _moveDirBuffer = new Position[4];

        public GameAction RecentGameAction => _gameActions.Count > 0 ? _gameActions.Peek() : null;

        public int Depth => _gameActions.Count;

        public GameState(GameMapState gameMapState)
        {
            _gameMapState = gameMapState;
        }

        public void Play(GameAction gameAction)
        {
            _gameActions.Push(gameAction);
            int cellIndex = _gameMapState.GetCellIndex(gameAction.Ship.X, gameAction.Ship.Y);
            _undoStack.Push(new GameMapUpdate(cellIndex, _gameMapState.Halite[cellIndex]));
            _gameMapState.Halite[cellIndex] = gameAction.OriginCellHalite;
        }

        public void Undo()
        {
            _gameActions.Pop();
            _gameMapState.ApplyUpdate(_undoStack.Pop());
        }

        public void UndoAll()
        {
            while (_gameActions.Count > 0)
            {
                Undo();
            }
        }

        public List<GameAction> GenerateChildrenActions(GameAction parent)
        {
            List<GameAction> results = new List<GameAction>(8)
            {
                GameAction.CreateStayStillAction(parent, _gameMapState.IsDropoffAt(parent))
            };

            int originCellHalite = _gameMapState.GetHaliteAt(parent.Ship.X, parent.Ship.Y);

            _moveDirBuffer[0] = new Position(parent.Ship.X, _gameMapState.ToNorthOf(parent.Ship.Y));
            _moveDirBuffer[1] = new Position(_gameMapState.ToLeftOf(parent.Ship.X), parent.Ship.Y);
            _moveDirBuffer[2] = new Position(_gameMapState.ToRightOf(parent.Ship.X), parent.Ship.Y);
            _moveDirBuffer[3] = new Position(parent.Ship.X, _gameMapState.ToSouthOf(parent.Ship.Y));

            for (int i = 0; i < _moveDirBuffer.Length; ++i)
            {
                Position pos = _moveDirBuffer[i];
                GameAction moveAction = GameAction.CreateMoveAction(
                    _moveDirOrder[i],
                    parent,
                    pos.X,
                    pos.Y,
                    originCellHalite,
                    _gameMapState.IsDropoffAt(pos));
                if (moveAction != null)
                {
                    results.Add(moveAction);
                }
            }

            return results;
        }
    }
}
