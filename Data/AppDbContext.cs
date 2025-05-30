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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasKey(l => l.Id);
            modelBuilder.Entity<DataRecord>().HasKey(d => d.Id);
            modelBuilder.Entity<Players>().HasKey(p => p.Id);
            modelBuilder.Entity<Match>().HasKey(m => m.Id);
            modelBuilder.Entity<Teams>().HasKey(t => t.Id);
            modelBuilder.Entity<Coach>().HasKey(c => c.Id);


            modelBuilder.Entity<Players>().HasData(
                new Players { Id = 1, Name = "Robert Lewandowski", Description = "Napastnik", Captain = true, Goals = 700, Team = "FC Barcelona" },
                new Players { Id = 2, Name = "Lionel Messi", Description = "Napastnik", Captain = false, Goals = 800, Team = "Paris Saint-Germain" },
                new Players { Id = 3, Name = "Cristiano Ronaldo", Description = "Napastnik", Captain = true, Goals = 750, Team = "Al Nassr" }
            );
            modelBuilder.Entity<Match>().HasData(
                new Match { Id = 1, Name = "FC Barcelona vs Real Madrid", Date = "2023-10-01", Team1 = "FC Barcelona", Team2 = "Real Madrid", Score1 = 3, Score2 = 1 },
                new Match { Id = 2, Name = "Paris Saint-Germain vs Manchester City", Date = "2023-10-02", Team1 = "Paris Saint-Germain", Team2 = "Manchester City", Score1 = 2, Score2 = 2 },
                new Match { Id = 3, Name = "Al Nassr vs Al Hilal", Date = "2023-10-03", Team1 = "Al Nassr", Team2 = "Al Hilal", Score1 = 1, Score2 = 0 }
            );
            modelBuilder.Entity<Teams>().HasData(
                new Teams { Id = 1, Name = "FC Barcelona", Description = "Hiszpański klub piłkarski", Captain = "Robert Lewandowski", Country = "Hiszpania", Points = 75 },
                new Teams { Id = 2, Name = "Real Madrid", Description = "Hiszpański klub piłkarski", Captain = "Karim Benzema", Country = "Hiszpania", Points = 70 },
                new Teams { Id = 3, Name = "Paris Saint-Germain", Description = "Francuski klub piłkarski", Captain = "Kylian Mbappé", Country = "Francja", Points = 80 }
            );
            modelBuilder.Entity<Coach>().HasData(
                new Coach { Id = 1, Name = "Xavi Hernandez", Description = "Trener FC Barcelona", Team = "FC Barcelona" },
                new Coach { Id = 2, Name = "Carlo Ancelotti", Description = "Trener Realu Madryt", Team = "Real Madrid" },
                new Coach { Id = 3, Name = "Christophe Galtier", Description = "Trener Paris Saint-Germain", Team = "Paris Saint-Germain" }
            );


            modelBuilder.Entity<Login>().HasData(
                new Login { Id = 1, Username = "robert", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test") },
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
        }
    }
}