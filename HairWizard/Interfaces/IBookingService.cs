using HairWizard.Models;
using HairWizard.Services;

namespace HairWizard.Interfaces
{
    public interface IBookingService
    {
        Booking? GetById(int id);
        List<Booking> GetAll();
        BookingResult Add(Booking booking);
        BookingResult Update(Booking booking);
        List<Booking> GetByUserId(string? userId);
        bool Delete(int id);
    }
}
