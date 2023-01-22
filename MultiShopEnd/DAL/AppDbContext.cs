using Microsoft.EntityFrameworkCore;
using MultiShopEnd.Models;

namespace MultiShopEnd.DAL
{
    public class AppDbContext:DbContext 

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }


    }
}
