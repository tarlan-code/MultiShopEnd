using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class Discount:BaseEntity
    {
        [StringLength(10)]
        public string Name { get; set; }
        [Range(0.0,100.0)]
        public double Percent { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
