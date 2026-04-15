using HairWizard.Data;
using HairWizard.Interfaces;
using HairWizard.Models;
using Microsoft.EntityFrameworkCore;

namespace HairWizard.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HairWizardContext _hairWizardContext;
        public BookingRepository(HairWizardContext hairWizardContext)
        {
            _hairWizardContext = hairWizardContext;
        }

        public List<Booking> GetByUserId(string? userId) =>
         _hairWizardContext.Bookings
        .Where(b => b.ApplicationUserId == userId)
        .Include(b => b.Employee)
             .Include(b => b.Treatment)
             .Include(b => b.ApplicationUser)
        .OrderByDescending(b => b.StartTime)
        .ToList();


        public Booking? GetById(int id)
        {
            var booking = _hairWizardContext.Bookings.
                Include(b => b.Employee)
                 .Include(b => b.Treatment)
                .FirstOrDefault(b => b.BookingId == id);
            return booking;
        }

        public List<Booking> GetAll()
        {
            var list = _hairWizardContext.Bookings
                .Include(b => b.Employee)
                 .Include(b => b.Treatment)
                .Include(b => b.ApplicationUser)
                .ToList();

            return list;
        }

        public void Add(Booking booking)
        {
            if (booking == null) return;

            _hairWizardContext.Bookings.Add(booking);
            _hairWizardContext.SaveChanges();
        }

        public void Update(Booking booking)
        {
            if (booking == null) return;

            var bookingToUpdate = _hairWizardContext.Bookings.Find(booking.BookingId);
            if (bookingToUpdate == null) return;

            if (booking.RowVersion != null)
            {
                _hairWizardContext.Entry(bookingToUpdate)
           .Property(b => b.RowVersion)
           .OriginalValue = booking.RowVersion;
            }

            bookingToUpdate.Treatment.Name = booking.Treatment.Name;
            bookingToUpdate.StartTime = booking.StartTime;
            bookingToUpdate.EndTime = booking.EndTime;
            bookingToUpdate.EmployeeId = booking.EmployeeId;

            _hairWizardContext.SaveChanges();

        }
        public void Delete(int id)
        {
            var booking = _hairWizardContext.Bookings.Find(id);
            if (booking != null)
            {
                _hairWizardContext.Bookings.Remove(booking);
                _hairWizardContext.SaveChanges();
            }
        }
    }
}
