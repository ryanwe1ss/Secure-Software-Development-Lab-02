using Lab_01.Models;
using Microsoft.AspNetCore.Identity;

public static class DbInitializer
{
    public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create roles if they don't exist
        await CreateRolesAsync(roleManager);

        // Seed users
        await SeedUserAsync(userManager, "manager@example.com", "123-456-7890", "Password123!", "Manager", "User");
        await SeedUserAsync(userManager, "employee@example.com", "123-456-7890", "Password456!", "Employee", "User");
    }

    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Manager"))
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
        }

        if (!await roleManager.RoleExistsAsync("Employee"))
        {
            await roleManager.CreateAsync(new IdentityRole("Employee"));
        }
    }

    private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string email, string phone, string password, string firstName, string lastName)
    {
        if (await userManager.FindByNameAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                NormalizedUserName = email,
                Email = email,
                PhoneNumber = phone,
                NormalizedEmail = email,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = true,
                AccessFailedCount = 0,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                BirthDate = DateTime.UtcNow,
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Assign user to a role based on email
                if (email.Equals("manager@example.com", StringComparison.OrdinalIgnoreCase))
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                }
                else if (email.Equals("employee@example.com", StringComparison.OrdinalIgnoreCase))
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
                else
                {
                    Console.WriteLine($"User {email} does not match any expected roles.");
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
        }
    }
}