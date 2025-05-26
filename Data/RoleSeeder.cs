using BibliotekaSzkolnaBlazor.Constants;
using Microsoft.AspNetCore.Identity;

namespace BibliotekaSzkolnaBlazor.Data
{
    public class RoleSeeder
    {
        //sprawdza czy dane role istnieja, jesli nie to tworzy je w bazie
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); //pobieramy Role Manager zeby uzywac do zarzadznia rolami

            //jesli dana rola nie istnieje to mozna stworzyc
            if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
            }

            if (!await roleManager.RoleExistsAsync(Roles.LIBRARIAN))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.LIBRARIAN));
            }

            if (!await roleManager.RoleExistsAsync(Roles.USER))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.USER));
            }

            //mozna zrobic jakas liste, petle itd ale to bez sensu dla 3 ról - wiec wystarczy, nie nalezy komplikowac kodu kiedy jest to zbedne
        }
    }
}
