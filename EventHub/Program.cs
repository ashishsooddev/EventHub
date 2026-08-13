using EventHub.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventHub.BLL.Services;

namespace EventHub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext")
            ?? throw new InvalidOperationException(
                "Connection string 'ApplicationDbContext' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages();

        builder.Services.AddScoped<EventService>();
        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<RegistrationService>();

        var app = builder.Build();

        SeedRolesAndUsersAsync(app.Services).GetAwaiter().GetResult();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }

    private static async Task SeedRolesAndUsersAsync(
        IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        RoleManager<IdentityRole> roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        UserManager<IdentityUser> userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Standard User" };

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        string adminEmail = "admin@eventhub.com";
        string adminPassword = "Admin123!";

        IdentityUser? adminUser =
            await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            IdentityResult result =
                await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        string userEmail = "user@eventhub.com";
        string userPassword = "User123!";

        IdentityUser? standardUser =
            await userManager.FindByEmailAsync(userEmail);

        if (standardUser == null)
        {
            standardUser = new IdentityUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true
            };

            IdentityResult result =
                await userManager.CreateAsync(standardUser,userPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(
                    standardUser,
                    "Standard User");
            }
        }
    }
}