using Microsoft.AspNetCore.Mvc;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
