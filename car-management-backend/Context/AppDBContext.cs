using car_management_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

    }
}
