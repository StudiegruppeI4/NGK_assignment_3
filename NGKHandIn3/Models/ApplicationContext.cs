using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGKHandIn3.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<WeatherObservation> WeatherObservations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder mb)
        {
            mb.Entity<WeatherObservation>().HasData
                (new WeatherObservation
                {
                    AirPressure = 2.8,
                    HumidityPercentage = 20,
                    Id = 1,
                    Latitude = 66.7,
                    Longitude = 90.2,
                    LocationName = "Florida",
                    Time = DateTime.Parse("01/01/2020"),
                    Temperature = 30
                }, new WeatherObservation
                {
                    AirPressure = 6.5,
                    HumidityPercentage = 60,
                    Id = 2,
                    Latitude = 77.7,
                    Longitude = 65.2,
                    LocationName = "Denmark",
                    Time = DateTime.Today.AddDays(-10),
                    Temperature = 65
                }, new WeatherObservation
                {
                    AirPressure = 2.8,
                    HumidityPercentage = 20,
                    Id = 3,
                    Latitude = 68.4,
                    Longitude = 90.2,
                    LocationName = "Sweden",
                    Time = DateTime.Today.AddHours(3),
                    Temperature = 30
                }, new WeatherObservation
                {
                    AirPressure = 8.7,
                    HumidityPercentage = 3,
                    Id = 4,
                    Latitude = 16.7,
                    Longitude = 90.2,
                    LocationName = "Belgium",
                    Time = DateTime.Today.AddHours(4),
                    Temperature = 30
                }, new WeatherObservation
                {
                    AirPressure = 90.8,
                    HumidityPercentage = 0,
                    Id = 5,
                    Latitude = 20.7,
                    Longitude = 43.2,
                    LocationName = "California",
                    Time = DateTime.Today.AddHours(5),
                    Temperature = 6
                }
                );
        }
    }
}
