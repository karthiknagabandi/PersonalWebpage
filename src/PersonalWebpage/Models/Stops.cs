using System;

namespace PersonalWebpage.Models
{
    public class Stops
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int Order { get; set; }
        public DateTime Arrival { get; set; }
    }
}