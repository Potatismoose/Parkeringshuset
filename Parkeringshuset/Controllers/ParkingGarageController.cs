using System.Collections.Generic;
using System.Linq;

namespace Parkeringshuset.Models
{
    class ParkingGarageController
    {
        private int MaxNoOfVehicles { get; set; } = 175;

        public List<ParkingMeterController> ParkingMeters = new();
        public int HourlyCost { get; set; }

        public ParkingGarageController(int hourlyCost)
        {
            HourlyCost = hourlyCost;
            ParkingMeters.Add(
                new ParkingMeterController("Main Floor", HourlyCost)
                );
        }

        public int CalculateAvailibleSpace()
        {
            List<int> Occupied = new();

            foreach (var parkingmeter in ParkingMeters)
            {
                var tickets = parkingmeter.GetActiveTickets();
                if (tickets != null)
                {
                    Occupied.Add(tickets.Count);
                }
            }

            return MaxNoOfVehicles - Occupied.Sum();
        }

        public bool IsCarParkedInGarageAlready(Vehicle vehicle)
        {

            foreach (var parkingmeter in ParkingMeters)
            {
                List<ParkingTicketController> activeTicket = new();
                if (parkingmeter.GetActiveTickets() != null)
                {
                    foreach (var ticket in parkingmeter.GetActiveTickets())
                    {
                        if (ticket.IsSameVehicle(vehicle))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
