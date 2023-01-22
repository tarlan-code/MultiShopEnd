using Microsoft.AspNetCore.Mvc;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
