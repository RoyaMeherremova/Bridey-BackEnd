using BrideyApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<AboutUs> AboutUss { get; set; }
        public DbSet<Bride> Brides { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<HeaderBackground> HeaderBackgrounds { get; set; }
        public DbSet<AboutBanner> AboutBanners { get; set; }
        public DbSet<Social> Socials { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HomeBanner>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<AboutUs>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Bride>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Team>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Position>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Advertising>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HeaderBackground>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Social>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HeaderBackground>().HasQueryFilter(m => !m.SoftDelete);

        }

    }
}
