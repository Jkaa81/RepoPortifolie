using HairWizard.Models;

namespace HairWizard.ViewModels
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; } = new Booking();

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public List<Treatment> Treatments { get; set; } = new();
    }
}
