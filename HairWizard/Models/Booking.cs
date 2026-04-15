using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HairWizard.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Treatment")]
        public int TreatmentId { get; set; }

        [BindNever]
        [ValidateNever]
        public Treatment Treatment { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        [BindNever]
        [ValidateNever]
        public Employee Employee { get; set; } = null!;

        [ValidateNever]
        public string? ApplicationUserId { get; set; }

        [BindNever]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
