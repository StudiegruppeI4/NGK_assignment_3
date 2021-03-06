using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NGKHandIn3.Controllers;
using NGKHandIn3.Models;
using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NGKHandIn3.Hubs;
using NSubstitute;

namespace WeatherObservations.Test.Unit
{
    public class WeatherObservationsUnitTests
    {
        private DbContextOptions<ApplicationContext> _options;
        private IHubContext<ObservationsHub> _hubContext;
        private SqliteConnection _connection;

        public WeatherObservationsUnitTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite(_connection).Options;
            _hubContext = Substitute.For<IHubContext<ObservationsHub>>();
        }

        [Fact]
        public void DataIsSeeded()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
                Assert.NotEmpty(context.WeatherObservations.ToList());
            }
        }

        [Fact]
        public async Task GetWeatherObservationsGet3()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();

                WeatherObservationsController uut = new WeatherObservationsController(context, _hubContext);

                var result = await uut.GetWeatherObservations();

                Assert.Equal(3, result.Value.Count());
            }
        }

        [Fact]
        public async Task GetWeatherObservationFromDateGet1()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();

                WeatherObservationsController uut = new WeatherObservationsController(context, _hubContext);

                var result = await uut.GetWeatherObservation(DateTime.Today.AddDays(-10));

                Assert.Equal("Denmark", result.Value.FirstOrDefault().LocationName);
            }
        }

        [Fact]
        public async Task GetWeatherObservationPeriodFromADateInterval()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
                WeatherObservationsController uut = new WeatherObservationsController(context, _hubContext);
                var result = await uut.GetWeatherObservationPeriod(DateTime.Today.AddDays(-11), DateTime.Today.AddDays(-10));
                Assert.Equal("Denmark", result.Value.FirstOrDefault().LocationName);
            }
        }

        [Fact]
        public async Task PostWeatherObservationAddsToDb()
        {
            using (var context = new ApplicationContext(_options))
            {
                context.Database.EnsureCreated();
                int initial = context.WeatherObservations.ToList().Count;
                WeatherObservationsController uut = new WeatherObservationsController(context, _hubContext);
                WeatherObservation entry = new WeatherObservation()
                {
                    LocationName = "Poland",
                    Temperature = 120
                };
                await uut.PostWeatherObservation(entry);
                Assert.Equal(initial + 1, context.WeatherObservations.ToList().Count);
            }
        }
    }
}
