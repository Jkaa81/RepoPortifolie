using System.ComponentModel.DataAnnotations;

namespace HairWizard.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Booking>? Bookings { get; set; }
    }
}
