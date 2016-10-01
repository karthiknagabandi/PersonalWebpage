using System.Collections.Generic;

namespace PersonalWebpage.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
    }
}