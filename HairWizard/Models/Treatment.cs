using System.ComponentModel.DataAnnotations;
namespace HairWizard.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int DurationInMinutes { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}
