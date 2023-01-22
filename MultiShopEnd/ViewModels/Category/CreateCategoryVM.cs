using System.ComponentModel.DataAnnotations;

namespace MultiShopEnd.ViewModels
{
    public class CreateCategoryVM
    {
        [StringLength(30)]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
