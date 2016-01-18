using SalemDance.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SalemDance.DAL
{
    public class DanceContext : DbContext
    {

        public DanceContext()
            : base("DanceContext")
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<DayOpen> DaysOpen { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<DanceStyle> DanceStyles { get; set; }
        public DbSet<DanceMove> DanceMoves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}