using Microsoft.EntityFrameworkCore;

namespace finalProject.Models
{
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
            // Relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Teams)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Characters)
                .WithOne(c => c.Team)
                .HasForeignKey(c => c.TeamId);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Moves)
                .WithOne(m => m.Character)
                .HasForeignKey(m => m.CharacterId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Badges)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Leaderboard>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Team1)
                .WithMany()
                .HasForeignKey(b => b.Team1Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes for Team1

            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Team2)
                .WithMany()
                .HasForeignKey(b => b.Team2Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes for Team2

            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Winner)
                .WithMany()
                .HasForeignKey(b => b.WinnerTeamId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes for Winner

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "JohnDoe", TotalWins = 15 },
                new User { UserId = 2, Username = "JaneDoe", TotalWins = 20 }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Madrid", UserId = 1, TotalWins = 10 },
                new Team { Id = 2, Name = "Barcelona", UserId = 2, TotalWins = 8 }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, Name = "Drone", Strength = 500, Defense = 300, Speed = 400, Health = 300, TeamId = 1 },
                new Character { Id = 2, Name = "Flight", Strength = 400, Defense = 200, Speed = 350, Health = 300, TeamId = 2 }
            );

            modelBuilder.Entity<Move>().HasData(
                new Move { Id = 1, Name = "Laser Beam", Power = 500, CharacterId = 1 },
                new Move { Id = 2, Name = "Sky Strike", Power = 600, CharacterId = 2 }
            );

            modelBuilder.Entity<Battle>().HasData(
                new Battle
                {
                    Id = 1,
                    Team1Id = 1,
                    Team2Id = 2,
                    WinnerTeamId = null, // No winner yet
                    BattleDate = new DateTime(2024, 1, 1)
                },
                new Battle
                {
                    Id = 2,
                    Team1Id = 2,
                    Team2Id = 1,
                    WinnerTeamId = null, // No winner yet
                    BattleDate = new DateTime(2024, 1, 15)
                }
            );

            modelBuilder.Entity<Badge>().HasData(
                new Badge { Id = 1, Name = "Champion", Description = "Awarded for winning 10 battles", UserId = 1 },
                new Badge { Id = 2, Name = "Veteran", Description = "Awarded for playing 50 battles", UserId = 2 }
            );

            modelBuilder.Entity<Leaderboard>().HasData(
                new Leaderboard { Id = 1, UserId = 1, Rank = 1, TotalWins = 15, SnapshotDate = new DateTime(2024, 12, 1) },
                new Leaderboard { Id = 2, UserId = 2, Rank = 2, TotalWins = 20, SnapshotDate = new DateTime(2024, 12, 1) }
            );
        }
    }
}