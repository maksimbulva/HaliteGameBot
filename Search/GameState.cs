using System.Collections.Generic;
using HaliteGameBot.Framework;
using HaliteGameBot.Search.GameActions;

namespace HaliteGameBot.Search
{
    class GameState
    {
        private readonly Game _game;
        private readonly Stack<IGameAction> _actions = new Stack<IGameAction>(32);

        public GameState(Game game)
        {
            _game = game;
        }

        public void Play(IGameAction action)
        {
            _actions.Push(action);
            action.Play(_game);
        }

        public void Undo()
        {
            IGameAction action = _actions.Pop();
            action.Undo(_game);
        }

        public void UndoAll()
        {
            while (_actions.Count > 0)
            {
                Undo();
            }
        }

        // TODO: consider using IEnumerable<IGameAction> here
        public List<IGameAction> GenerateActions(Ship ship)
        {
            List<IGameAction> results = new List<IGameAction>(8)
            {
                new StayStill(_game, ship)
            };

            var moveUp = new MoveY(_game, ship, MoveY.MoveDir.UP);
            if (moveUp.MoveCost <= ship.Halite)
            {
                results.Add(moveUp);
            }

            var moveDown = new MoveY(_game, ship, MoveY.MoveDir.DOWN);
            if (moveDown.MoveCost <= ship.Halite)
            {
                results.Add(moveDown);
            }

            var moveLeft = new MoveX(_game, ship, MoveX.MoveDir.LEFT);
            if (moveLeft.MoveCost <= ship.Halite)
            {
                results.Add(moveLeft);
            }

            var moveRight = new MoveX(_game, ship, MoveX.MoveDir.RIGHT);
            if (moveRight.MoveCost <= ship.Halite)
            {
                results.Add(moveRight);
            }

            return results;
        }
    }
}
