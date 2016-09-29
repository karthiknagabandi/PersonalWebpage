using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PersonalWebpage.Models
{
    public class WorldContext : DbContext
    {
        private IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options): 
            base(options)
        {
            _config = config;

        }
        //class that represents the entity
        //gives us a class on which we can execute LINQ queries
        //starting point for queriable interfaces
        //use this when we start to query the DB directly
        //we will have to register this in startup -- configure services
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stops> Stops { get; set; }

        // to create a DB connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // TODO: check why the config variable is not working
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TheWorld;Trusted_Connection=true;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
