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
            List<IGameAction> results = new List<IGameAction>(8);
            results.Add(new StayStill(ship));
            return results;
        }
    }
}
