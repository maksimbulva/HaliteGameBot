using System;
using System.Linq;
using HaliteGameBot.Framework;

namespace HaliteGameBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Constants.Init(Console.ReadLine());
            Game game = Game.CreateFromInput();

            Log.SetFileName($"bot-{game.MyPlayer.Id}.log");

            MyBot bot = new MyBot(game);
            bot.Initialize();

            // The Halite game process is waiting for output from us
            // This indicates the end of the initilization stage and starts the game
            Console.WriteLine("HaliteGameBot");

            while (true)
            {
                game.ReadFrameUpdate();
                var commands = bot.GenerateTurnCommands();
                string turnOutput = string.Join(" ", commands.Select(command => command.ToString()).ToArray());
                Console.WriteLine(turnOutput);
                Console.Out.Flush();
                bot.OnMoveCompleted();
            }
        }
    }
}
