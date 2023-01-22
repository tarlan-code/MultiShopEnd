using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class Size:BaseEntity
    {
        [StringLength(30)]
        public string Name { get; set; }
        public ICollection<ProductSize>? ProductSizes { get; set; }
        public bool IsDeleted { get; set; }

    }
}
