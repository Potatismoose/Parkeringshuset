namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;

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
