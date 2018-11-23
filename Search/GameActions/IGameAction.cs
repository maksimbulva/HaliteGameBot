namespace HaliteGameBot.Search.GameActions
{
    internal interface IGameAction
    {
        void Play(GameMapState gameMapState);
        void Undo(GameMapState gameMapState);
    }
}
