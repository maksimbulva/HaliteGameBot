using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;
using HaliteGameBot.Search;
using System;

namespace HaliteGameBot
{
    internal static class CommandFactory
    {
        public static ICommand FromGameAction(GameActions gameActionType, int entityId)
        {
            switch (gameActionType)
            {
                case GameActions.STAY_STILL:
                    return new Move(entityId, Direction.STAY_STILL);
                case GameActions.MOVE_NORTH:
                    return new Move(entityId, Direction.NORTH);
                case GameActions.MOVE_SOUTH:
                    return new Move(entityId, Direction.SOUTH);
                case GameActions.MOVE_WEST:
                    return new Move(entityId, Direction.WEST);
                case GameActions.MOVE_EAST:
                    return new Move(entityId, Direction.EAST);
            }
            throw new NotImplementedException();
        }
    }
}
