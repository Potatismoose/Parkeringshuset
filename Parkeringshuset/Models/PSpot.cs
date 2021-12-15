namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;

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