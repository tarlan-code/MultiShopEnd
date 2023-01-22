using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class ProductInfo:BaseEntity
    {
        [StringLength(20)]
        public string Name { get; set; }
        [MinLength(10),MaxLength(800)]
        public string Text { get; set; }
        public bool IsDeleted { get; set; }

    }
}
