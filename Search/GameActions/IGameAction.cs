using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    interface IGameAction
    {
        void Play(Game game);
        void Undo(Game game);
    }
}
