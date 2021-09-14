using System;

namespace Parkeringshuset.Models
{
    public class ParkingTicket
    {
        private Vehicle AssociatedVehicle;
        private DateTime ArrivalTime;
        private DateTime? CheckoutTime = null;
        private int PricePerStartedHour;
        private int CostOfParking;

        public ParkingTicket(Vehicle associatedVehicle, int hourlyCost)
        {
            ArrivalTime = DateTime.Now;
            AssociatedVehicle = associatedVehicle;
            PricePerStartedHour = hourlyCost;
        }

        public void Checkout()
        {
            CheckoutTime = DateTime.Now;
            CalculateCost();
        }

        public void CalculateCost()
        {
            var time = CheckoutTime - ArrivalTime;
        }

        public bool TicketIsStillActive()
        {
            return !CheckoutTime.HasValue;
        }

        public bool IsSameVehicle(Vehicle vehicle)
        {

            return string.Equals(
                AssociatedVehicle.ToString().ToLower(),
                vehicle.ToString().ToLower()
                );
        }
    }
}
