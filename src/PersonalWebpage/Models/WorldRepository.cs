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

        //return collection of trips
       public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all the trips from database");
            return _context.Trips.ToList();
        }

    }
}
