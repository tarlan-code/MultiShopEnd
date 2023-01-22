using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.ViewModels
{
    public class UpdateCategoryVM
    {

        [StringLength(30)]
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
