using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TheEventsApp;
using TheEventsApp.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class Program {
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("EventsDb")
            )
        );

      

 

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();



        var app = builder.Build();

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

        app.UseAuthorization();
        app.MapRazorPages();



        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService <RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByEmailAsync("root@example.com") == null)
            {
                var user = new ApplicationUser();

                user.UserName = "root@example.com";
                user.FirstName = "Root";
                user.LastName = "Admin";
                user.Email = "root@example.com";

                await userManager.CreateAsync(user, "Root123$");
                await userManager.AddToRoleAsync(user, "Admin");
            }


        }
        app.Run();
    }
}
