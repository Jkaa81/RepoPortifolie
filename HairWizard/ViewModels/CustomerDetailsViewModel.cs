
using HairWizard.Models;

namespace HairWizard.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public ApplicationUser Customer { get; set; } = null!;
        public List<Booking> UpcomingBookings { get; set; } = new();
    }
}

