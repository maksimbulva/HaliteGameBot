using System.Collections.Generic;

namespace HaliteGameBot.Framework
{
    class Game
    {
        public int TurnNumber { get; private set; }

        public Player MyPlayer { get; private set; }
        public List<Player> Players { get; private set; }

        public GameMap GameMap { get; private set; }

        public static Game CreateFromInput()
        {
            Game game = new Game();
            game.ReadPlayersFromInput();
            game.GameMap = GameMap.CreateFromInput();
            return game;
        }

        private Game()
        {
        }

        public void ReadFrameUpdate()
        {
            var reader = new InputReader();
            TurnNumber = reader.NextInt();

            Log.Write($"=============== TURN {TurnNumber} ================");

            SetMapCellsOccupiedStatus(false);

            for (int i = 0; i < Players.Count; ++i)
            {
                var playerReader = new InputReader();
                int id = playerReader.NextInt();
                int shipCount = playerReader.NextInt();
                int dropoffCount = playerReader.NextInt();
                int halite = playerReader.NextInt();
                Players[id].UpdateFromInput(shipCount, dropoffCount, halite);
            }

            GameMap.UpdateFromInput();

            SetMapCellsOccupiedStatus(true);

                // TODO
                /*        const Shipyard& shipyard = player.shipyard();
                        game_map->at(shipyard.position())->structure =
                            std::make_shared<Shipyard>(shipyard.playerId(), shipyard.position());
                */

            // for (const auto&dropoff : player.dropoffs()) {
                        // TODO
                        // game_map->at(dropoff)->structure = dropoff;
                    // }
                // }
            // }
        }

        private void ReadPlayersFromInput()
        {
            var reader = new InputReader();

            int playerCount = reader.NextInt();
            int myPlayerId = reader.NextInt();

            Players = new List<Player>(playerCount);
            for (; playerCount > 0; --playerCount)
            {
                Players.Add(Factory.CreatePlayerFromInput());
            }

            MyPlayer = Players[myPlayerId];
        }

        private void SetMapCellsOccupiedStatus(bool status)
        {
            foreach (Player player in Players)
            {
                player.Ships.ForEach(ship => GameMap.Occupied[GameMap.GetIndex(ship.Position)] = status);
            }
        }
    }
}
