using Microsoft.EntityFrameworkCore;

namespace finalProject.Models
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Battle> Battles { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Teams)
                .WithOne(t => t.User);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Characters)
                .WithOne(c => c.Team);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Moves)
                .WithOne(m => m.Character);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Badges)
                .WithOne(b => b.User);
        }
    }

}
