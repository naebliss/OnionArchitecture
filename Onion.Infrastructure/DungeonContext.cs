using Microsoft.EntityFrameworkCore;
using Onion.Core;

namespace Onion.Infrastructure
{
    public class DungeonContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "in-memory");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hero>();
            builder.Entity<Ork>();
            builder.Entity<Skeleton>();
            builder.Entity<Rabbit>();

            base.OnModelCreating(builder);
        }

        public DbSet<Dungeon> Dungeons { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Monster> Monsters{ get; set; }
    }
}
