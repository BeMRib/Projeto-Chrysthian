using Games.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=games.sqlite;Cache=Shared");
    }
}