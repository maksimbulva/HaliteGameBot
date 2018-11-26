using System;
using System.Linq;
using HaliteGameBot.Framework;

namespace HaliteGameBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Constants.Init(Console.ReadLine());
            GameMechanicsHelper.Init();

            Game game = Game.CreateFromInput();

            Log.SetFileName($"bot-{game.MyPlayer.Id}.log");

            GameMapGeometry.InitDimensions(game.GameMap);

            MyBot bot = new MyBot(game);
            bot.Initialize();

            // The Halite game process is waiting for output from us
            // This indicates the end of the initilization stage and starts the game
            Console.WriteLine("HaliteGameBot");

            while (true)
            {
                game.ReadFrameUpdate();
                Log.Write("Ships: " + string.Join(", ", game.MyPlayer.Ships));
                var commands = bot.GenerateTurnCommands();
                string turnOutput = string.Join(" ", commands.Select(command => command.ToString()).ToArray());
                Console.WriteLine(turnOutput);
                Console.Out.Flush();
                bot.OnMoveCompleted();
            }
        }
    }
}
