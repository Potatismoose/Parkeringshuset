namespace Parkeringshuset.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PTicket
    {
        [Key]
        public int Id { get; set; }

        public DateTime CheckedInTime { get; set; } 

        public DateTime CheckedOutTime { get; set; }

        public int Cost { get; set; }

        public bool IsPaid { get; set; }

        public Vehicle Vehicle  { get; set; }

        public PType Type  { get; set; }

        


    }

}
