using System.Collections.Generic;

namespace HaliteGameBot.Framework
{
    internal sealed class Player
    {
        public int Id { get; private set; }
        public int Halite { get; private set; }
        public Shipyard Shipyard { get; private set; }

        public List<Ship> Ships { get;  }
        public List<Dropoff> Dropoffs { get; }

        public Player(int id, Position shipyardPosition)
        {
            Id = id;
            Shipyard = new Shipyard(Id, shipyardPosition);

            Ships = new List<Ship>();
            Dropoffs = new List<Dropoff>();
        }

        public void UpdateFromInput(int shipCount, int dropoffCount, int halite)
        {
            Halite = halite;

            Ships.Clear();
            for (; shipCount > 0; --shipCount)
            {
                Ships.Add(Factory.CreateShipFromInputLine(Id));
            }

            Dropoffs.Clear();
            for (; dropoffCount > 0; --dropoffCount)
            {
                Dropoffs.Add(Factory.CreateDropoffFromInputLine(Id));
            }
        }
    }
}
