namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Index(nameof(Name), IsUnique = true)]
    public class PType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Used { get; set; }

        public int TotalSpots { get; set; }
    }
}
