
using HairWizard.Data;
using HairWizard.Interfaces;
using HairWizard.Models;
using HairWizard.Persistence;
using HairWizard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HairWizard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();

            //Add EF Core DI:
            builder.Services.AddDbContext<HairWizardContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Register Identity services
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<HairWizardContext>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsEnvironment("Testing"))
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<HairWizard.Data.HairWizardContext>();
                    db.Database.Migrate();
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bookings}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}



