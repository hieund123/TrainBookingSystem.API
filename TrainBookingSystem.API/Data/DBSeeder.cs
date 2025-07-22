using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TrainBookingSystem.API.Data
{
    public static class DBSeeder
    {
        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Passenger", ConcurrencyStamp = "2", NormalizedName = "PASSENGER" }
            );
        }
    }
}
