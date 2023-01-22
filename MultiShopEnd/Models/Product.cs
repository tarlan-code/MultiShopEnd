using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class Product:BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(700)]
        public string Desc { get; set; }
        [Range(0.0,1000.0)]
        public double SellPrice { get; set; }
        [Range(0.0, 1000.0)]
        public double CostPrice { get; set; }
        [Range(0.0,5.0)]
        public double Rating { get; set; }
        public bool IsDeleted { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int ProductInfoId { get; set; }
        public ProductInfo productInfo { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductSize>? ProductSizes { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
