namespace Parkeringshuset.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class PSpot
    {

        public int Id { get; set; }

        public int Motorbice { get; set; } = 10;

        public int Electric { get; set; } = 30;

        public int Handicap { get; set; } = 10;

        public int Regular { get; set; } = 120;

        public int Monthly { get; set; } = 30;

    }
}
