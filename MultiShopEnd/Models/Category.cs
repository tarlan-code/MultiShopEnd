using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class Category:BaseEntity
    {
        [StringLength(30)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
