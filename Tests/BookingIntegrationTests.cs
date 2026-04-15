using System.Net;

namespace HairWizard.Tests
{
    public class BookingIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public BookingIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_BookingsPage_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Bookings");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}