using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Models;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sizes.Where(c => c.Id >1));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            Size size = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (size is null) return NotFound();
            foreach (var item in _context.ProductColors.Where(pc => pc.ColorId == id))
                item.ColorId = 1; //Default Size Id 
            _context.Sizes.Remove(size);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid) return View();
            _context.Sizes.Add(size);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            Size size = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (size is null) return NotFound();
            return View(size);
        }
        [HttpPost]
        public IActionResult Update(int? id, Size size)
        {
            if (id is null || id <= 1) return BadRequest();
            if (!ModelState.IsValid) return View();
            Size oldSize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (oldSize is null) return NotFound();
            oldSize.Name = size.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 1) return BadRequest();
            Size size = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (size is null) return NotFound();
            if (size.IsDeleted) size.IsDeleted = false;
            else size.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
