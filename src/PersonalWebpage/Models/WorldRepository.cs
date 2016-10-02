using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebpage.Models
{
    public class WorldRepository :IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stops newStop)
        {
            var trip = GetTripByName(tripName);
            if(trip !=null)
            {
                //Settings as Foreign Key
                trip.Stops.Add(newStop);
                //Adding Actual Value to the DB
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
             _context.Add(trip);
        }

        //return collection of trips
        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all the trips from database");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t=> t.Name == tripName)
                .FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //public async Task<bool> SaveChangesAsync()
        //{
        //    return (await _context.SaveChangesAsync()) > 0;
        //}
    }
}
