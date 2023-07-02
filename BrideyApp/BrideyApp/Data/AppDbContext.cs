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
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<SectionBackgroundImage> SectionBackgroundImages { get; set; }
        public DbSet<AboutBox> AboutBoxes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<ProductComposition> ProductCompositions { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Sale> Sales { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HomeBanner>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<AboutBanner>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<AboutUs>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Bride>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Team>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Position>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Advertising>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HeaderBackground>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Setting>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Social>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<HeaderBackground>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Author>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<BlogComment>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<SectionBackgroundImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<AboutBox>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Size>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Color>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Composition>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductSize>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductComposition>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductColor>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ProductCategory>().HasQueryFilter(m => !m.SoftDelete);

        }

    }
}
