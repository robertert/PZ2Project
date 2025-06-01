using Microsoft.EntityFrameworkCore;
using LoginApp.Models;

namespace LoginApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Login>? Logins { get; set; }
        public DbSet<DataRecord>? DataRecords { get; set; }

        public DbSet<Players>? Players { get; set; }

        public DbSet<Match>? Match { get; set; }
        public DbSet<Teams>? Teams { get; set; }
        public DbSet<Coach>? Coach { get; set; }
        public DbSet<Login> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasKey(l => l.Id);
            modelBuilder.Entity<DataRecord>().HasKey(d => d.Id);
            modelBuilder.Entity<Players>().HasKey(p => p.Id);
            modelBuilder.Entity<Match>().HasKey(m => m.Id);
            modelBuilder.Entity<Teams>().HasKey(t => t.Id);
            modelBuilder.Entity<Coach>().HasKey(c => c.Id);


            modelBuilder.Entity<Players>().HasData(
                new Players { Id = 1, Name = "Robert Lewandowski", Description = "Napastnik", Captain = true, Goals = 700, TeamId = 1 },
                new Players { Id = 2, Name = "Lionel Messi", Description = "Napastnik", Captain = false, Goals = 800, TeamId = 3 },
                new Players { Id = 3, Name = "Cristiano Ronaldo", Description = "Napastnik", Captain = true, Goals = 750, TeamId = 2 }
            );
            modelBuilder.Entity<Match>().HasData(
                new Match { Id = 1, Name = "FC Barcelona vs Real Madrid", Date = new DateTime(2023, 10, 1), Team1Id = 1, Team2Id = 2, Score1 = 3, Score2 = 1 },
                new Match { Id = 2, Name = "Paris Saint-Germain vs Manchester City", Date = new DateTime(2023, 10, 2), Team1Id = 3, Team2Id = 4, Score1 = 2, Score2 = 2 },
                new Match { Id = 3, Name = "Al Nassr vs Al Hilal", Date = new DateTime(2023, 10, 3), Team1Id = 5, Team2Id = 6, Score1 = 1, Score2 = 0 }
            );

            modelBuilder.Entity<Teams>().HasData(
                new Teams { Id = 1, Name = "FC Barcelona", Description = "Hiszpański klub piłkarski", Captain = "Robert Lewandowski", Country = "Hiszpania", Points = 75 },
                new Teams { Id = 2, Name = "Real Madrid", Description = "Hiszpański klub piłkarski", Captain = "Karim Benzema", Country = "Hiszpania", Points = 70 },
                new Teams { Id = 3, Name = "Paris Saint-Germain", Description = "Francuski klub piłkarski", Captain = "Kylian Mbappé", Country = "Francja", Points = 80 },
                new Teams { Id = 5, Name = "Al Nassr", Description = "Saudyjski klub piłkarski", Captain = "Cristiano Ronaldo", Country = "Arabia Saudyjska", Points = 65 },
                new Teams { Id = 6, Name = "Al Hilal", Description = "Saudyjski klub piłkarski", Captain = "Salem Al-Dawsari", Country = "Arabia Saudyjska", Points = 70 }
            );
            modelBuilder.Entity<Coach>().HasData(
                new Coach { Id = 1, Name = "Xavi Hernandez", Description = "Trener FC Barcelona", TeamId = 1 },
                new Coach { Id = 2, Name = "Carlo Ancelotti", Description = "Trener Realu Madryt", TeamId = 2 },
                new Coach { Id = 3, Name = "Christophe Galtier", Description = "Trener Paris Saint-Germain", TeamId = 3 }
            );



            modelBuilder.Entity<Login>().HasData(
                new Login { Id = 1, Username = "robert", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = "Admin" },
                new Login { Id = 2, Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("userpass") },
                new Login { Id = 3, Username = "deweloper", PasswordHash = BCrypt.Net.BCrypt.HashPassword("haslo123") },
                new Login { Id = 4, Username = "deweloper2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("haslo12345") }
            );

            modelBuilder.Entity<DataRecord>().HasData(
                new DataRecord { Id = 1, Content = "Przykładowe dane 1" },
                new DataRecord { Id = 2, Content = "Przykładowe dane 2" },
                new DataRecord { Id = 3, Content = "Przykładowe dane 3" },
                new DataRecord { Id = 4, Content = "Przykładowe dane 4" }
            );

            modelBuilder.Entity<Players>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Team)
                .WithMany(t => t.Coaches)
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team1)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.Team1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Team2)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.Team2Id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}