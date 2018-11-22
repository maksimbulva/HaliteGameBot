using HaliteGameBot.Framework;
using HaliteGameBot.Framework.Commands;
using HaliteGameBot.Search.GameActions;
using System;

namespace HaliteGameBot
{
    static class CommandFactory
    {
        public static ICommand FromAction(IGameAction gameAction, int entityId)
        {
            if (gameAction == null || gameAction is StayStill)
            {
                return new Move(entityId, Direction.STAY_STILL);
            }
            var moveXAction = gameAction as MoveX;
            if (moveXAction != null)
            {
                Direction direction = moveXAction.DeltaX == -1 || moveXAction.DeltaX > 1
                    ? Direction.WEST
                    : Direction.EAST;
                return new Move(entityId, direction);
            }
            var moveYAction = gameAction as MoveY;
            if (moveYAction != null)
            {
                Direction direction = moveYAction.DeltaY == -1 || moveYAction.DeltaY > 2
                    ? Direction.NORTH
                    : Direction.SOUTH;
            }
            throw new NotImplementedException();
        }
    }
}
