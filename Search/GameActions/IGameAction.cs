using HaliteGameBot.Framework;

namespace HaliteGameBot.Search.GameActions
{
    internal interface IGameAction
    {
        Position Destination { get; }
        void Play(PlayerState playerState, GameMapState gameMapState);
        void Undo(PlayerState playerState, GameMapState gameMapState);
    }
}
