using HairWizard.Data;
using HairWizard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairWizard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly HairWizardContext _context;

        public CustomersController(HairWizardContext context)
        {
            _context = context;
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var customer = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (customer == null)
                return NotFound();

            var upcomingBookings = _context.Bookings
                .Include(b => b.Employee)
                .Include(b => b.Treatment)
                .Where(b => b.ApplicationUserId == id && b.StartTime > DateTime.Now)
                .OrderBy(b => b.StartTime)
                .ToList();

            var vm = new CustomerDetailsViewModel
            {
                Customer = customer,
                UpcomingBookings = upcomingBookings
            };

            return View(vm);
        }
    }
}