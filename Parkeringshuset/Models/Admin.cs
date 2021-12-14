namespace Parkeringshuset.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Index(nameof(Username), nameof(Email), IsUnique = true)]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }
    }
}
