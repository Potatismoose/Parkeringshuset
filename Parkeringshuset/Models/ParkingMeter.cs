using System;
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

        public ParkingTicket CreateTicket(Vehicle vechicle)
        {
            ParkingTickets.Add(new ParkingTicket(vechicle, HourlyCost));
            return ParkingTickets[^1];
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

        public IEnumerable<ParkingTicket> GetAllSoldTickets(DateTime startDate, DateTime endDate)
        {
            return ParkingTickets.ToList()
                .Where(x =>
                x.IsPayed() == true
                && x.ReturnArrivalTime() >= startDate
                && x.ReturnCheckoutTime() < endDate.AddDays(1));
        }

        public (bool checkOutSuccessful, ParkingTicket ticket) CheckoutVehicle(Vehicle vehicle)
        {
            ParkingTicket checkedOutVechicle = null;
            var ticket = ParkingTickets.FirstOrDefault(x =>
                x.TicketIsStillActive()
                && x.IsSameVehicle(vehicle));
            if (ticket is not null)
            {
                checkedOutVechicle = ticket.Checkout();
                return (true, checkedOutVechicle);
            }
            return (false, checkedOutVechicle);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
