using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.ViewModels;

namespace MultiShopEnd.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM();   
            vm.CarouselItems = _context.CarouselItems.Where(c=>c.IsDeleted == false).OrderBy(c=>c.Order).ToList();
            return View(vm);
        }
        
    }
}