using HairWizard.Interfaces;
using HairWizard.Models;
namespace HairWizard.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public BookingService(IBookingRepository bookingRepository, ITreatmentRepository treatmentRepository)
        {
            _bookingRepository = bookingRepository;
            _treatmentRepository = treatmentRepository;
        }

        public List<Booking> GetAll() => _bookingRepository.GetAll().
                                                            Where(b => b.StartTime > DateTime.Now)
                                                            .OrderBy(b => b.StartTime)
                                                            .ToList();


        public BookingResult Add(Booking booking)
        {
            var treatment = _treatmentRepository.GetById(booking.TreatmentId);
            if (treatment == null)
                return BookingResult.Fail(nameof(booking.TreatmentId), "Please select a valid treatment");

            booking.EndTime = booking.StartTime.AddMinutes(treatment.DurationInMinutes);

            if (booking.StartTime < DateTime.Now)
                return BookingResult.Fail(nameof(booking.StartTime), "Start time cannot be in the past");

            var overlaps = _bookingRepository.GetAll()
                .Where(x => x.EmployeeId == booking.EmployeeId)
                .Any(x => booking.StartTime < x.EndTime && booking.EndTime > x.StartTime);

            if (overlaps)
                return BookingResult.Fail(nameof(booking.EmployeeId), "Employee is not available for the selected time");

            _bookingRepository.Add(booking);
            return BookingResult.Ok();

        }
        public BookingResult Update(Booking booking)
        {

            var treatment = _treatmentRepository.GetById(booking.TreatmentId);
            if (treatment == null)
                return BookingResult.Fail(nameof(booking.TreatmentId), "Please select a valid treatment");

            booking.EndTime = booking.StartTime.AddMinutes(treatment.DurationInMinutes);

            if (booking.StartTime < DateTime.Now)
                return BookingResult.Fail(nameof(booking.StartTime), "Start time cannot be in the past");

            var overlaps = _bookingRepository.GetAll().Any(x =>
                x.EmployeeId == booking.EmployeeId &&
                x.BookingId != booking.BookingId &&
                booking.StartTime < x.EndTime &&
                booking.EndTime > x.StartTime);

            if (overlaps)
                return BookingResult.Fail(nameof(booking.EmployeeId), "Employee is not available for the selected time");

            _bookingRepository.Update(booking);
            return BookingResult.Ok();
        }
        public bool Delete(int id)
        {
            if (_bookingRepository.GetById(id) is null) return false;
            _bookingRepository.Delete(id);
            return true;
        }


        public Booking? GetById(int id) => _bookingRepository.GetById(id);


        public List<Booking> GetByUserId(string? userId) => _bookingRepository.GetByUserId(userId)
                                                                                .Where(b => b.StartTime > DateTime.Now)
                                                                                .OrderBy(b => b.StartTime)
                                                                                .ToList();
    }
}

