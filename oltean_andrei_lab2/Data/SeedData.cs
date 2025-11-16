using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Areas.Identity.Data;
using oltean_andrei_lab2.Data;

namespace oltean_andrei_lab2.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryIdentityContext(
                       serviceProvider.GetRequiredService<DbContextOptions<LibraryIdentityContext>>()))
            {
                if (context.Roles.Any())
                {
                    return; 
                }

                context.Roles.AddRange(
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "Client", NormalizedName = "CLIENT" }
                );
                context.SaveChanges();
            }
        }
    }
}