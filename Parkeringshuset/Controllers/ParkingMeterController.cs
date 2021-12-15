//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Parkeringshuset.Models
//{
//    class ParkingMeterController
//    {

//        private string Name { get; set; }
//        List<ParkingTicketController> ParkingTickets;
//        public int HourlyCost { get; }

//        public ParkingMeterController(string name, int hourlyCost)
//        {
//            Name = name;
//            ParkingTickets = new();
//            HourlyCost = hourlyCost;
//        }

//        public ParkingTicketController CreateTicket(Vehicle vechicle)
//        {
//            ParkingTickets.Add(new ParkingTicketController(vechicle, HourlyCost));
//            return ParkingTickets[^1];
//        }

//        public List<ParkingTicketController> GetActiveTickets()
//        {
//            if (ParkingTickets.Count > 0)
//            {
//                return ParkingTickets.Where(x =>
//                x.TicketIsStillActive())
//                .ToList();
//            }

//            return null;
//        }

//        public IEnumerable<ParkingTicketController> GetAllSoldTickets(DateTime startDate,
//        DateTime endDate)
//        {
//            return ParkingTickets.ToList()
//                .Where(x =>
//                x.IsPayed() == true
//                && x.ReturnArrivalTime() >= startDate
//                && x.ReturnCheckoutTime() < endDate.AddDays(1));
//        }

//        public (bool checkOutSuccessful, ParkingTicketController ticket)
//        CheckoutVehicle(Vehicle vehicle)
//        {
//            ParkingTicketController checkedOutVechicle = null;
//            var ticket = ParkingTickets.FirstOrDefault(x =>
//                x.TicketIsStillActive()
//                && x.IsSameVehicle(vehicle));
//            if (ticket is not null)
//            {
//                checkedOutVechicle = ticket.Checkout();
//                return (true, checkedOutVechicle);
//            }
//            return (false, checkedOutVechicle);
//        }

//        public override string ToString()
//        {
//            return Name;
//        }
//    }
//}
