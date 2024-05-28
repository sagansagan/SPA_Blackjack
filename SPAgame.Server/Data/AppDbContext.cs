using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPAgame.Server.Models;

namespace SPAgame.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlackjackGame> BlackjackGames { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<PlayerHand> PlayerHands { get; set; }
        public DbSet<DealerHand> DealerHands { get; set; }
        public DbSet<HighScore> HighScores { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
