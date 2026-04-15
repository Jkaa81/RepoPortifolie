using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HairWizard.Models
{


    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public List<Booking>? Bookings { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
