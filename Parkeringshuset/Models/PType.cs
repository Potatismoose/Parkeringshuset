namespace Parkeringshuset.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class PType
    {
        [Key]
        public int Id { get; set; }

        public string Handicap { get; set; }

        public string Regular { get; set; }

        public string Motorbike { get; set; }
    }
}
