using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTest.Models;
using TimeTest.Models.Clients;

namespace TimeTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Time> Times { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }

    }
}
