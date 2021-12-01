using Parkeringshuset.Data;
using Parkeringshuset.Helper;
using System;

namespace Parkeringshuset.Models
{
    public class ParkingTicketController
    {
        public ParkeringGarageContext db = new();

        #region Create
        public bool CreateTicket(string regNr,string type)
        {
            try
            {
                PTicket ticket = new PTicket();
                
                Vehicle vehicle = new Vehicle() { RegistrationNumber = regNr };

                ticket.Vehicle = vehicle;
               
                ticket.IsPaid = false;
                ticket.CheckedInTime = DateTime.Now;

                db.Ptickets.Add(ticket);
                db.SaveChanges();
                return true;
            }
            catch 
            {   
                return false;   
            }

        }
#endregion 



        

    }
}




//private Vehicle AssociatedVehicle;
//private DateTime ArrivalTime;
//private DateTime? CheckoutTime = null;
//private int PricePerStartedHour;


//public ParkingTicketController(Vehicle associatedVehicle, int hourlyCost)
//{
//    ArrivalTime = DateTime.Now;
//    AssociatedVehicle = associatedVehicle;
//    PricePerStartedHour = hourlyCost;
//}

//public ParkingTicketController Checkout()
//{
//    CheckoutTime = DateTime.Now;
//    CalculateCost();
//    return this;
//}

//public int CalculateCost()
//{
//    TimeSpan time = new(1, 0, 0);
//    time += Convert.ToDateTime(CheckoutTime) - ArrivalTime;
//    return time.Hours * PricePerStartedHour;
//}

//public bool TicketIsStillActive()
//{
//    return !CheckoutTime.HasValue;
//}

//public bool IsSameVehicle(Vehicle vehicle)
//{
//    return string.Equals(
//        AssociatedVehicle.ToString(),
//        vehicle.ToString(),
//        StringComparison.OrdinalIgnoreCase);
//}

//public bool IsPayed()
//{
//    return CheckoutTime.HasValue;
//}

//public DateTime ReturnArrivalTime()
//{
//    return ArrivalTime;
//}

//public DateTime? ReturnCheckoutTime()
//{
//    return CheckoutTime;
//}

//public override string ToString()
//{
//    var cost = 0;
//    if (CheckoutTime.HasValue)
//    {
//        return $"Biljett. " +
//            $"Regnr: {AssociatedVehicle}, " +
//            $"{(cost = CalculateCost()) / PricePerStartedHour}tim " +
//            $"á {PricePerStartedHour}kr. " +
//            $"\tTot {cost}kr";
//    }
//    else 
//    {
//        return $"Regnr: {AssociatedVehicle}, Hourly cost: {PricePerStartedHour}kr. Parking started: {ArrivalTime} ";
//    }
//}
