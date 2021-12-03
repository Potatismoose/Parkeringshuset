using Parkeringshuset.Data;
using Parkeringshuset.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parkeringshuset.Models
{
    public class ParkingTicketController
    {
        public ParkeringGarageContext db = new();

        public int CostOfParking { get; private set; }

        #region Create
        /// <summary>
        /// Creates a new ticket in database and set the checkedInTime to now. 
        /// </summary>
        /// <param name="regNr">Of the checked in car.</param>
        /// <param name="type">Type of Vehicle. Make user choose between garages type options.
        /// </param>
        /// <returns></returns>
        public bool CreateTicket(string regNr, PType type)
        {
            try
            {
                PTicket ticket = new();
                Vehicle vehicle = new() { RegistrationNumber = regNr };
                ticket.Vehicle = vehicle;
                ticket.IsPaid = false;
                ticket.Type = type;
                ticket.CheckedInTime = DateTime.Now;
                ticket.CheckedOutTime = DateTime.MinValue;
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

        /// <summary>
        /// Checks out the vehicle and sets the CheckedOutTime.
        /// </summary>
        /// <param name="regNr">The reqNr of the vehicle.</param>
        /// <returns>True if checking out is successfull and false if the vehicle is not checked in
        /// or the regNr is wrong.</returns>
        public bool CheckOut(string regNr)
        {
            var vehicle = db.Vehicles.FirstOrDefault(x => x.RegistrationNumber == regNr);

            if (vehicle == null)
            {
                return false;
            }
            var ticket = db.Ptickets.FirstOrDefault(
                x => x.Vehicle.Id == vehicle.Id && x.CheckedOutTime == DateTime.MinValue);

            ticket.CheckedOutTime = DateTime.Now;
            db.Ptickets.Update(ticket);
            db.SaveChanges();

            return true;
        }

        /// <summary>
        /// Gets the active ticket for the regNr.
        /// </summary>
        /// <param name="regNr">The reqNr of the vehicle.</param>
        /// <returns>A ticket if the vehicle is not checked out and null if the regNr is wrong or
        /// does not exist.</returns>
        public PTicket GetActiveTicket(string regNr)
        {
            var vehicle = db.Vehicles.FirstOrDefault(x => x.RegistrationNumber == regNr);

            if (vehicle == null){
                return null;
            }

            var ticket = db.Ptickets.FirstOrDefault(
                x => x.Vehicle.Id == vehicle.Id && x.CheckedOutTime == DateTime.MinValue);

            return ticket;
        }

        /// <summary>
        /// Checks if the ticket is active.
        /// </summary>
        /// <param name="ticket">The ticket to check.</param>
        /// <returns>True if the ticket is active, otherwise false.</returns>
        public bool IsTicketActive(PTicket ticket){
            var t = db.Ptickets.FirstOrDefault(x => x.CheckedOutTime == DateTime.MinValue);
            return ticket.CheckedOutTime == t.CheckedOutTime;
        }

        // public bool IsPayed(string regNr){
        //     var vehicle = db.Vehicles.FirstOrDefault(x => x.RegistrationNumber == regNr);

        //     if (vehicle == null)
        //     {
        //         return false;
        //     }

        //     var ticket = db.Ptickets.FirstOrDefault(x => x.Vehicle.Id == vehicle.Id);
        //     if (IsTicketActive(ticket)){
        //         if (!ticket.IsPaid)
        //         {
        //             return false;
        //         }
        //         return false;
        //     }
        //     return true;
        // }

        /// <summary>
        /// Check if the ticket is monthly.
        /// </summary>
        /// <param name="regNr">The reqNr of the vehicle.</param>
        /// <returns>True if the ticket is monthly otherwise false.</returns>
        public bool IsMonthly(string regNr){
            var vehicle = db.Vehicles.FirstOrDefault(x => x.RegistrationNumber == regNr);

            if (vehicle == null)
            {
                return false;
            }

            var ticket = db.Ptickets.FirstOrDefault(x => x.Vehicle.Id == vehicle.Id);

            return ticket.Type.Name == "Monthly";
        }
    }
}