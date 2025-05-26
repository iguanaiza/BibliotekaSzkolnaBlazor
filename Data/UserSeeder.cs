using BibliotekaSzkolnaBlazor.Constants;
using Microsoft.AspNetCore.Identity;

namespace BibliotekaSzkolnaBlazor.Data
{
    public class UserSeeder
    {
        //seeder do tworzenia uzytkownikow - podstawowe 3 role w celach testowych
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userManager, "admin", "Admin123!", Roles.ADMIN); //tworzy konto testowe admina
            await CreateUserWithRole(userManager, "librarian", "Librarian123!", Roles.LIBRARIAN); //tworzy konto testowe bilbiotekarza
            await CreateUserWithRole(userManager, "user", "User123!", Roles.USER); //tworzy konto testowe usera
        }

        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password); //tworzy usera oraz nadaje haslo

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role); //nadanie roli
                }

                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}"); //handling bledow (wielu)
                }
            }
        }
    }
}
