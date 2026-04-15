using HairWizard.Interfaces;
using HairWizard.Models;
using HairWizard.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairWizard.Controllers
{

    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IBookingService _bookingService;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BookingsController(
            IEmployeeRepository roomRepository,
            IBookingService bookingService,
            ITreatmentRepository treatmentRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _employeeRepository = roomRepository;
            _bookingService = bookingService;
            _treatmentRepository = treatmentRepository;
            _usermanager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = _usermanager.GetUserId(User);
                var bookings = _bookingService.GetByUserId(userId);

                return View(bookings);
            }
            else
            {
                var bookings = _bookingService.GetAll();
                return View(bookings);
            }
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            var now = DateTime.Now;

            var start = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddHours(1);

            var bookingVM = new BookingViewModel
            {
                Booking = new Booking
                {
                    StartTime = start,
                },
                Employees = _employeeRepository.GetAll(),
                Treatments = _treatmentRepository.GetAll()
            };

            return View(bookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookingViewModel bookingVM)
        {
            ViewBag.Action = "add";

            if (!ModelState.IsValid)
            {
                bookingVM.Employees = _employeeRepository.GetAll();
                bookingVM.Treatments = _treatmentRepository.GetAll();
                return View(bookingVM);
            }
            var user = await _usermanager.GetUserAsync(User);
            bookingVM.Booking.ApplicationUserId = user!.Id;

            var result = _bookingService.Add(bookingVM.Booking);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(result.Key ?? string.Empty, result.ErrorMessage);
                bookingVM.Employees = _employeeRepository.GetAll();
                bookingVM.Treatments = _treatmentRepository.GetAll();
                return View(bookingVM);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";

            var booking = _bookingService.GetById(id);
            if (booking is null) return NotFound();

            var bookingVM = new BookingViewModel
            {
                Booking = booking,
                Employees = _employeeRepository.GetAll(),
                Treatments = _treatmentRepository.GetAll()
            };
            return View(bookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookingViewModel bookingVM)
        {
            ViewBag.Action = "edit";

            if (!ModelState.IsValid)
            {
                bookingVM.Employees = _employeeRepository.GetAll();
                bookingVM.Treatments = _treatmentRepository.GetAll();

                return View(bookingVM);
            }

            try
            {
                var updateResult = _bookingService.Update(bookingVM.Booking);

                if (!updateResult.IsSuccessful)
                {
                    ModelState.AddModelError(updateResult.Key ?? string.Empty, updateResult.ErrorMessage);
                    bookingVM.Employees = _employeeRepository.GetAll();
                    bookingVM.Treatments = _treatmentRepository.GetAll();
                    return View(bookingVM);
                }

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "Booking blev ændret af en anden bruger. Genindlæs siden og prøv igen");
                bookingVM.Employees = _employeeRepository.GetAll();
                return View(bookingVM);
            }
        }

        public IActionResult Delete(int id)
        {
            _bookingService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
