namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;

        [Index(nameof(RegistrationNumber), IsUnique = true)]
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
    }
}