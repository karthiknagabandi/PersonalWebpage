using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalWebpage.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUserName(string name);
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stops newStop, string username);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string name);
    }
}