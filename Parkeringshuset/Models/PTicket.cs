namespace Parkeringshuset.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PTicket
    {
        [Key]
        public int Id { get; set; }

        public DateTime CheckedInTime { get; set; } 

        public DateTime CheckedOutTime { get; set; }

        public int Cost { get; set; }

        public bool IsPaid { get; set; }

        public bool isActice { get; set; }

        public Vehicle Vehicle  { get; set; }

        public PType Type  { get; set; }
    }
}
