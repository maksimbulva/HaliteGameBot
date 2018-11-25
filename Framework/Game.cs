using System.Collections.Generic;

namespace HaliteGameBot.Framework
{
    internal sealed class Game
    {
        public int TurnNumber { get; private set; }

        public Player MyPlayer { get; private set; }
        public List<Player> Players { get; private set; }

        public GameMap GameMap { get; private set; }

        public Dictionary<int, Ship> MyShips { get; }

        private readonly List<GameMapUpdate> _mapUpdates = new List<GameMapUpdate>(128);
        public IEnumerable<GameMapUpdate> MapUpdates { get => _mapUpdates; }

        public static Game CreateFromInput()
        {
            Game game = new Game();
            game.ReadPlayersFromInput();
            game.GameMap = GameMap.CreateFromInput();
            return game;
        }

        private Game()
        {
            MyShips = new Dictionary<int, Ship>(32);
        }

        public void ReadFrameUpdate()
        {
            var reader = new InputReader();
            TurnNumber = reader.NextInt();

            Log.Write($"=============== TURN {TurnNumber} ================");

            for (int i = 0; i < Players.Count; ++i)
            {
                var playerReader = new InputReader();
                int id = playerReader.NextInt();
                int shipCount = playerReader.NextInt();
                int dropoffCount = playerReader.NextInt();
                int halite = playerReader.NextInt();
                Players[id].UpdateFromInput(shipCount, dropoffCount, halite);
            }

            ReadMapUpdatesFromInput();
            GameMap.Update(_mapUpdates);

            UpdateMyShipsDict();

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

        private void ReadMapUpdatesFromInput()
        {
            var reader = new InputReader();
            for (int updateCount = reader.NextInt(); updateCount > 0; --updateCount)
            {
                _mapUpdates.Clear();
                var updateReader = new InputReader();
                int x = updateReader.NextInt();
                int y = updateReader.NextInt();
                int halite = updateReader.NextInt();
                _mapUpdates.Add(new GameMapUpdate(GameMap.GetIndex(x, y), halite));
            }
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

        private void UpdateMyShipsDict()
        {
            MyShips.Clear();
            MyPlayer.Ships.ForEach(ship => MyShips.Add(ship.EntityId, ship));
        }
    }
}
