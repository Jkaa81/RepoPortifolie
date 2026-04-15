using HairWizard.Interfaces;
using HairWizard.Models;
using HairWizard.Services;
using Moq;

namespace HairWizard.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public void Add_ShouldReturnFailure_WhenStartTimeIsInThePast()
        {
            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var treatmentRepositoryMock = new Mock<ITreatmentRepository>();

            treatmentRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(new Treatment { TreatmentId = 1, DurationInMinutes = 60 });

            bookingRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Booking>());

            var service = new BookingService(
                bookingRepositoryMock.Object,
                treatmentRepositoryMock.Object
            );

            var booking = new Booking
            {
                TreatmentId = 1,
                EmployeeId = 1,
                StartTime = DateTime.Now.AddHours(-1)
            };

            var result = service.Add(booking);

            Assert.False(result.IsSuccessful);
            Assert.Equal(nameof(booking.StartTime), result.Key);
            Assert.Equal("Start time cannot be in the past", result.ErrorMessage);
        }

        [Fact]
        public void Add_ShouldAddBooking_WhenBookingIsValid()
        {
            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var treatmentRepositoryMock = new Mock<ITreatmentRepository>();

            treatmentRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(new Treatment { TreatmentId = 1, DurationInMinutes = 60 });

            bookingRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Booking>());

            var service = new BookingService(
                bookingRepositoryMock.Object,
                treatmentRepositoryMock.Object
            );

            var booking = new Booking
            {
                TreatmentId = 1,
                EmployeeId = 1,
                StartTime = DateTime.Now.AddHours(2)
            };

            var result = service.Add(booking);

            Assert.True(result.IsSuccessful);
            Assert.Equal(booking.StartTime.AddMinutes(60), booking.EndTime);
            bookingRepositoryMock.Verify(x => x.Add(booking), Times.Once);
        }

        [Fact]
        public void Add_ShouldReturnFailure_WhenTreatmentDoesNotExist()
        {
            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var treatmentRepositoryMock = new Mock<ITreatmentRepository>();

            treatmentRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns((Treatment?)null);

            var service = new BookingService(
                bookingRepositoryMock.Object,
                treatmentRepositoryMock.Object
            );

            var booking = new Booking
            {
                TreatmentId = 1,
                EmployeeId = 1,
                StartTime = DateTime.Now.AddHours(2)
            };

            var result = service.Add(booking);

            Assert.False(result.IsSuccessful);
            Assert.Equal(nameof(booking.TreatmentId), result.Key);
            Assert.Equal("Please select a valid treatment", result.ErrorMessage);
        }
    }
}