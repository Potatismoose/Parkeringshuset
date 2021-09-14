using System.Collections.Generic;
using System.Linq;

namespace Parkeringshuset.Models
{
    class ParkingMeter
    {

        private string Name { get; set; }
        List<ParkingTicket> ParkingTickets;
        public int HourlyCost { get; }

        public ParkingMeter(string name, int hourlyCost)
        {
            Name = name;
            ParkingTickets = new();
            HourlyCost = hourlyCost;
        }

        public void CreateTicket(Vehicle vechicle)
        {
            ParkingTickets.Add(new ParkingTicket(vechicle, HourlyCost));
        }

        public List<ParkingTicket> GetActiveTickets()
        {
            if (ParkingTickets.Count > 0)
            {
                return ParkingTickets.Where(x =>
                x.TicketIsStillActive())
                .ToList();
            }

            return null;
        }

        public void GetAllSoldTickets(Vehicle vechicle)
        {
            ParkingTickets.Add(new ParkingTicket(vechicle, HourlyCost));
        }

        public bool CheckoutVehicle(Vehicle vehicle)
        {
            var ticket = ParkingTickets.FirstOrDefault(x =>
                x.TicketIsStillActive()
                && x.IsSameVehicle(vehicle));
            if (ticket is not null)
            {
                ticket.Checkout();
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
