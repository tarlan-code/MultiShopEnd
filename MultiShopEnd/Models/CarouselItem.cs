using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class CarouselItem:BaseEntity
    {
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        [StringLength(15)]
        public string BtnText { get; set; }
        [Url]
        public string BtnUrl { get; set; }
        [Range(1,int.MaxValue)]
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
    }
}
