using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.Models
{
    public class OfferItem:BaseEntity
    {
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(20)]
        public string SecondTitle { get; set; }
        [StringLength(15)]
        public string BtnText { get; set; }
        [Url]
        public string BtnUrl { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,4)]
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
    }
}
