using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class Color:BaseEntity
    {
        [StringLength(30)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
