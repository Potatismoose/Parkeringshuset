using System.Collections.Generic;
using System.Linq;

namespace Parkeringshuset.Models
{
    class ParkingGarage
    {
        private int MaxNoOfVehicles { get; set; } = 175;

        public List<ParkingMeter> ParkingMeters = new();
        public int HourlyCost { get; set; }

        public ParkingGarage(int hourlyCost)
        {
            HourlyCost = hourlyCost;
            ParkingMeters.Add(
                new ParkingMeter("Main Floor", HourlyCost)
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
                List<ParkingTicket> activeTicket = new();
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
