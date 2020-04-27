﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGKHandIn3.Models;

namespace NGKHandIn3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherObservationsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public WeatherObservationsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/WeatherObservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservations()
        {
            var result =  await _context.WeatherObservations.OrderByDescending(model => model.Time).Take(3).ToListAsync();
            return result;
        }

        // GET: api/WeatherObservations/datetime
        [HttpGet("{dateTime}")]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservations(string dateTime)
        {
            DateTime time = DateTime.Parse(dateTime);
            var weatherObservation = await _context.WeatherObservations.Where(wo => wo.Time == time).ToListAsync();

            if (weatherObservation == null)
            {
                return NotFound();
            }
            return weatherObservation;
        }

        // GET: api/WeatherObservationPeriod/start-end
        [HttpGet("{start}-{end}")]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservationPeriod(DateTime start, DateTime end)
        {
            var weatherObservation = await _context.WeatherObservations.Where(wo => wo.Time < end && wo.Time > start).ToListAsync();

            if (weatherObservation == null)
            {
                return NotFound();
            }
            return weatherObservation;
        }

        // POST: api/WeatherObservations
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.WeatherObservations.Add(weatherObservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherObservation", new { id = weatherObservation.Id }, weatherObservation);
        }
    }
}