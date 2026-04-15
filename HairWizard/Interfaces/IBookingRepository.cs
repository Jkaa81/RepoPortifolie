using HairWizard.Models;

namespace HairWizard.Interfaces
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        void Delete(int id);
        List<Booking> GetAll();
        List<Booking> GetByUserId(string? userId);
        Booking? GetById(int id);
        void Update(Booking booking);

    }
}
