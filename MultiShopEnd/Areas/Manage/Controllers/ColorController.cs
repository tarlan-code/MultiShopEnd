using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Models;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Colors.Where(c=>c.Id>1));
        }

        public IActionResult Delete(int? id)
        {
            if(id is null || id<=1) return BadRequest();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if(color is null) return NotFound();
            foreach (var item in _context.ProductColors.Where(pc => pc.ColorId == id))
                item.ColorId = 1; //Default Color Id 
            _context.Colors.Remove(color);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid) return View();
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if(id is null || id <=1) return BadRequest();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }
        [HttpPost]
        public IActionResult Update(int? id,Color color)
        {
            if (id is null || id <= 1) return BadRequest();
            if (!ModelState.IsValid) return View();
            Color oldColor = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (oldColor is null) return NotFound();
            oldColor.Name = color.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 1) return BadRequest();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            if (color.IsDeleted) color.IsDeleted = false;
            else color.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
