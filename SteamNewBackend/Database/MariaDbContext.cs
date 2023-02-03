using Microsoft.EntityFrameworkCore;
using SteamNewBackend.Models;

namespace SteamNewBackend.Database
{
    public class MariaDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.User).WithMany(u => u.UserGames).HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGame>().HasOne(ug => ug.Game).WithMany(g => g.UserGames).HasForeignKey(ug => ug.GameId);

            //one Game to many Posts relation
            modelBuilder.Entity<Post>().HasOne(p => p.Game)
                .WithMany(g => g.Posts).HasForeignKey(p => p.Game_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<DevTeam> DevTeams { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
    }
}
