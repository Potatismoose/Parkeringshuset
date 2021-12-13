namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Index(nameof(Motorbike),nameof(Electric),nameof(Handicap),nameof(Regular), 
           nameof(Monthly), IsUnique = true)]
    public class PSpot
    {

        public int Id { get; set; }

        public int Motorbike { get; set; }

        public int Electric { get; set; }

        public int Handicap { get; set; }

        public int Regular { get; set; } 

        public int Monthly { get; set; }

    }
}
