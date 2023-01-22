using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.ViewModels
{
    public class CreateCarouselVM
    {
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Desc { get; set; }
        public IFormFile Image { get; set; }
        [StringLength(15)]
        public string BtnText { get; set; }
        [Url]
        public string BtnUrl { get; set; }
        [Range(1, int.MaxValue)]
        public int Order { get; set; }
    }
}
