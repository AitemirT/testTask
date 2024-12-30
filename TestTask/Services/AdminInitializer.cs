using Microsoft.AspNetCore.Identity;
using TestTask.Models;

namespace TestTask.Services;

public class AdminInitializer
{
    public static async Task SeedRolesAndAdmin(RoleManager<IdentityRole<int>> roleManager, UserManager<User> _userManager)
    {
        string adminEmail = "admin@admin.com";
        string adminPassword = "1qwe@QWE";
        string adminUserName = "admin";
        DateOnly dateOfBirth = DateOnly.FromDateTime(DateTime.Now);

        var roles = new[] { "admin", "user" };
        
        foreach (var role in roles)
        {
            if (await roleManager.FindByNameAsync(role) is null)
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        if (await _userManager.FindByNameAsync(adminEmail) == null)
        {
            User admin = new User { Email = adminEmail, UserName = adminUserName, DateOfBirth = dateOfBirth};
            IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(admin, "admin");
        }
    }
}