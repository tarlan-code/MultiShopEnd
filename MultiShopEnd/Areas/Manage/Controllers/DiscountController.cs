using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Models;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class DiscountController : Controller
    {
        private readonly AppDbContext _context;

        public DiscountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Discounts.Where(pi => pi.Id > 1));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            Discount discount = _context.Discounts.FirstOrDefault(c => c.Id == id);
            if (discount is null) return NotFound();
            foreach (var item in _context.Products.Where(pc => pc.ProductInfoId == id))
                item.ProductInfoId = 1; //Default Discount Id 
            _context.Discounts.Remove(discount);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Discount discount)
        {
            if (!ModelState.IsValid) return View();
            _context.Discounts.Add(discount);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            Discount discount = _context.Discounts.FirstOrDefault(c => c.Id == id);
            if (discount is null) return NotFound();
            return View(discount);
        }
        [HttpPost]
        public IActionResult Update(int? id, Discount discount)
        {
            if (id is null || id <= 1) return BadRequest();
            if (!ModelState.IsValid) return View();
            Discount oldDiscount = _context.Discounts.FirstOrDefault(c => c.Id == id);
            if (oldDiscount is null) return NotFound();
            oldDiscount.Name = discount.Name;
            oldDiscount.Percent = discount.Percent;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 1) return BadRequest();
            Discount discount = _context.Discounts.FirstOrDefault(c => c.Id == id);
            if (discount is null) return NotFound();
            if (discount.IsDeleted) discount.IsDeleted = false;
            else discount.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



    }
}

