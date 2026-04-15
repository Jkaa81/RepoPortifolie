using HairWizard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HairWizard.Data
{
    public class HairWizardContext : IdentityDbContext<ApplicationUser>
    {

        public HairWizardContext(DbContextOptions<HairWizardContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Treatment> Treatments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Treatment)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TreatmentId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(r => r.Bookings)
            .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.ApplicationUserId);

            // SEED EMPLOYEES
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Sofie" },
                new Employee { EmployeeId = 2, Name = "Emma" },
                new Employee { EmployeeId = 3, Name = "Freja" },
                new Employee { EmployeeId = 4, Name = "Ida" }
            );

            // SEED TREATMENTS
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { TreatmentId = 1, Name = "Haircut", DurationInMinutes = 30 },
                new Treatment { TreatmentId = 2, Name = "Coloring", DurationInMinutes = 120 },
                new Treatment { TreatmentId = 3, Name = "Styling", DurationInMinutes = 45 },
                new Treatment { TreatmentId = 4, Name = "Beard Trim", DurationInMinutes = 20 }
);

            // SEED BOOKINGS (uden bruger for nu)
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    TreatmentId = 1,
                    StartTime = new DateTime(2026, 4, 20, 9, 0, 0),
                    EndTime = new DateTime(2026, 4, 20, 10, 0, 0),
                    EmployeeId = 1
                },
                new Booking
                {
                    BookingId = 2,
                    TreatmentId = 3,
                    StartTime = new DateTime(2026, 4, 20, 10, 30, 0),
                    EndTime = new DateTime(2026, 4, 20, 12, 0, 0),
                    EmployeeId = 2
                },
                new Booking
                {
                    BookingId = 3,
                    TreatmentId = 4,
                    StartTime = new DateTime(2026, 4, 21, 13, 0, 0),
                    EndTime = new DateTime(2026, 4, 21, 14, 0, 0),
                    EmployeeId = 3
                }
                );


        }



    }
}
