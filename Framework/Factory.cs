namespace HaliteGameBot.Framework
{
    static class Factory
    {
        private static readonly Commands.SpawnShip _spawnShipCommand = new Commands.SpawnShip();

        static public Dropoff CreateDropoffFromInputLine(int playerId) {
            var reader = new InputReader();
            int entityId = reader.NextInt();
            int x = reader.NextInt();
            int y = reader.NextInt();
            return new Dropoff(playerId, entityId, new Position(x, y));
        }

        static public Player CreatePlayerFromInput()
        {
            var reader = new InputReader();
            int playerId = reader.NextInt();
            int shipyardX = reader.NextInt();
            int shipyardY = reader.NextInt();
            return new Player(playerId, new Position(shipyardX, shipyardY));
        }

        static public Ship CreateShipFromInputLine(int playerId)
        {
            var reader = new InputReader();
            int entityId = reader.NextInt();
            int x = reader.NextInt();
            int y = reader.NextInt();
            int halite = reader.NextInt();
            return new Ship(playerId, entityId, new Position(x, y), halite);
        }

        static public Commands.Move CreateMoveCommand(Ship ship, Direction direction)
            => new Commands.Move(ship.EntityId, direction);

        static public Commands.SpawnShip CreateSpawnShipCommand() => _spawnShipCommand;
    }
}
