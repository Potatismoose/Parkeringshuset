using Microsoft.EntityFrameworkCore;
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
        public bool CreateTicket(string regNr, string type)
        {
            try
            {
                PTicket ticket = new();
                var existingVehicle = db.Vehicles.FirstOrDefault(x => x.RegistrationNumber == regNr);

                if (existingVehicle is not null)
                {
                    ticket.Vehicle = existingVehicle;
                }
                else
                {
                    ticket.Vehicle = new Vehicle() { RegistrationNumber = regNr };
                }

                ticket.IsPaid = false;
                ticket.Type = db.Ptypes.FirstOrDefault(x => x.Name == type);
                ticket.Type.Used += 1;
                ticket.CheckedInTime = DateTime.Now;

                if (type == PTypesNamesHelper.Monthly)
                {
                    ticket.CheckedOutTime = DateTime.Now.AddDays(30);
                }
                else
                {
                    ticket.CheckedOutTime = DateTime.MinValue;
                }

                ticket.isActice = true;
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
        /// <param name="ticket">The reqNr of the vehicle.</param>
        /// <returns>True if checking out is successfull, otherwise false.</returns>
        public bool CheckOut(PTicket ticket)
        {
            var t = db.Ptickets.Include(x => x.Type).FirstOrDefault(x => x.Id == ticket.Id);
            var pt = db.Ptypes.FirstOrDefault(x=> t.Type.Name == x.Name);

            t.CheckedOutTime = DateTime.Now;
            t.isActice = false;
            pt.Used -= 1;
            db.Ptickets.Update(t);
            db.Ptypes.Update(pt);
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

            if (vehicle == null)
            {
                return null;
            }

            var ticket = db.Ptickets.Include(x => x.Type).Include(y => y.Vehicle).FirstOrDefault(
                x => x.Vehicle.Id == vehicle.Id && x.isActice == true); 

            return ticket;
        }

        /// <summary>
        /// Checks if the ticket is active.
        /// </summary>
        /// <param name="ticket">The ticket to check.</param>
        /// <returns>True if the ticket is active, otherwise false.</returns>
        public bool IsTicketActive(PTicket ticket)
        {
            var t = db.Ptickets.FirstOrDefault(x => x.Id == ticket.Id);

            if (t == null)
            {
                return false;
            }

            return t.isActice;
        }

        /// <summary>
        /// Checks if the ticket is payed.
        /// </summary>
        /// <param name="ticket">The ticket to check.</param>
        /// <returns>True if the ticket is payed, otherwise false.</returns>
        public bool IsPayed(PTicket ticket)
        {
            var t = db.Ptickets.FirstOrDefault(x => x.Id == ticket.Id);
            if (IsTicketActive(ticket) || !t.IsPaid)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if the ticket is monthly.
        /// </summary>
        /// <param name="ticket">The ticket to check.</param>
        /// <returns>True if the ticket is monthly otherwise false.</returns>
        public bool IsMonthly(PTicket ticket)
        {
            var t = db.Ptickets.FirstOrDefault(x => x.Id == ticket.Id);

            if (t?.Type == null)
            {
                return false;
            }

            return t.Type.Name == PTypesNamesHelper.Monthly;  
        }

        /// <summary>
        /// Update Admin object in database.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>true if successfull, false if database issues.</returns>
        public bool Update(PTicket ticket)
        {
            try
            {
                db.Update(ticket);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(PTicket ticket)
        {
            try
            {
                ticket = db.Ptickets.FirstOrDefault(x => x.Id == ticket.Id);
                db.Ptickets.Remove(ticket);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}