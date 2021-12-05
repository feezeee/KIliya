
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DBConector : DbContext
    {
        public DBConector(DbContextOptions<DBConector> options) : base(options)
        {

        }

        public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<SitPlace> SitPlaces { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainDestination> TrainDestinations { get; set; }
        public DbSet<Van> Vans { get; set; }
        public DbSet<TrainVanSit> TrainVanSits { get; set; }
        public DbSet<Worker> Workers { get; set; }

    }
}
