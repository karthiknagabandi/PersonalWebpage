using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalWebpage.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stops newStop);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
       
    }
}