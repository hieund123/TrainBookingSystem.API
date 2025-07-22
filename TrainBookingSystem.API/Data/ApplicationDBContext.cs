using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Models.Tables;

namespace TrainBookingSystem.API.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        // DbSet cho các bảng trong hệ thống đặt vé
        public DbSet<TrainStation> TrainStations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<CarriageClass> CarriageClasses { get; set; }
        public DbSet<TrainJourney> TrainJourneys { get; set; }
        public DbSet<JourneyStation> JourneyStations { get; set; }
        public DbSet<JourneyCarriage> JourneyCarriages { get; set; }
        public DbSet<CarriagePrice> CarriagePrices { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Gọi seed roles nếu có
            builder.SeedRoles();

            // Cấu hình khóa chính phức hợp
            builder.Entity<JourneyStation>()
                .HasKey(js => new { js.TrainStationId, js.TrainJourneyId });

            builder.Entity<CarriagePrice>()
                .HasKey(cp => new { cp.ScheduleId, cp.CarriageClassId });

            // Cấu hình quan hệ Booking - IdentityUser
            builder.Entity<Booking>()
                .HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh multiple cascade

            // Cấu hình quan hệ với TrainStation
            builder.Entity<Booking>()
                .HasOne(b => b.StartingTrainStation)
                .WithMany()
                .HasForeignKey(b => b.StartingTrainStationId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh multiple cascade

            builder.Entity<Booking>()
                .HasOne(b => b.EndingTrainStation)
                .WithMany()
                .HasForeignKey(b => b.EndingTrainStationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình decimal chính xác
            builder.Entity<CarriagePrice>()
                .Property(cp => cp.Price)
                .HasPrecision(18, 2);

            builder.Entity<Booking>()
                .Property(b => b.AmountPaid)
                .HasPrecision(18, 2);
        }
    }
}
