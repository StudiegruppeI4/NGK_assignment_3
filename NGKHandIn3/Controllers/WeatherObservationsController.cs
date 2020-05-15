using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGKHandIn3.Hubs;
using NGKHandIn3.Models;

namespace NGKHandIn3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherObservationsController : ControllerBase
    {
        private readonly IHubContext<ObservationsHub> _hubContext;
        private readonly ApplicationContext _context;

        public WeatherObservationsController(ApplicationContext context, IHubContext<ObservationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/WeatherObservations
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservations()
        {
            var result =  await _context.WeatherObservations.OrderByDescending(model => model.Time).Take(3).ToListAsync();
            return result;
        }

        // GET: api/WeatherObservations/datetime
        [HttpGet("{dateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservation(DateTime dateTime)
        {
            var weatherObservation = await _context.WeatherObservations
                .Where(wo => wo.Time.DayOfYear == dateTime.DayOfYear && wo.Time.Year == dateTime.Year)
                .ToListAsync();

            if (weatherObservation == null)
            {
                return NotFound();
            }
            return weatherObservation;
        }

        // GET: api/WeatherObservationPeriod/start-end
        [HttpGet("{start}:{end}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservationPeriod(DateTime start, DateTime end)
        {
            var weatherObservation = await _context.WeatherObservations
                .Where(wo => wo.Time.DayOfYear <= end.DayOfYear && wo.Time.DayOfYear >= start.DayOfYear && wo.Time.Year <= end.Year && wo.Time.Year >= start.Year)
                .ToListAsync();

            if (weatherObservation == null)
            {
                return NotFound();
            }
            return weatherObservation;
        }

        // POST: api/WeatherObservations
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.WeatherObservations.Add(weatherObservation);
            await _context.SaveChangesAsync();
            string weatherObservationFormatted = "Weatherobservation\n" +
                                                 $"Time: {weatherObservation.Time.ToLongTimeString()} - {weatherObservation.Time.ToLongDateString()}\n" +
                                                 $"Location: {weatherObservation.LocationName}\n" +
                                                 $"Latitude/Longitude: {weatherObservation.Latitude}/{weatherObservation.Longitude}\n" +
                                                 $"Temperature: {weatherObservation.Temperature}\n" +
                                                 $"Humidity: {weatherObservation.HumidityPercentage}\n" +
                                                 $"Air Pressure: {weatherObservation.AirPressure}";
            await _hubContext.Clients.All.SendAsync("NewPost", weatherObservationFormatted);
            return CreatedAtAction("GetWeatherObservation", new { DateTime = weatherObservation.Time.DayOfYear }, weatherObservation);
        }
    }
}
