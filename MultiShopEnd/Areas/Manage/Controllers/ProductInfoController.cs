using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShopEnd.DAL;
using MultiShopEnd.Models;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class ProductInfoController : Controller
    {
        private readonly AppDbContext _context;

        public ProductInfoController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.ProductInfos.Where(pi => pi.Id > 1));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            ProductInfo productInfo = _context.ProductInfos.FirstOrDefault(c => c.Id == id);
            if (productInfo is null) return NotFound();
            foreach (var item in _context.Products.Where(pc => pc.ProductInfoId == id))
                item.ProductInfoId = 1; //Default ProductInfo Id 
            _context.ProductInfos.Remove(productInfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductInfo productInfo)
        {
            if (!ModelState.IsValid) return View();
            _context.ProductInfos.Add(productInfo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if (id is null || id <= 1) return BadRequest();
            ProductInfo productInfo = _context.ProductInfos.FirstOrDefault(c => c.Id == id);
            if (productInfo is null) return NotFound();
            return View(productInfo);
        }
        [HttpPost]
        public IActionResult Update(int? id, ProductInfo productInfo)
        {
            if (id is null || id <= 1) return BadRequest();
            if (!ModelState.IsValid) return View();
            ProductInfo oldProductInfo = _context.ProductInfos.FirstOrDefault(c => c.Id == id);
            if (oldProductInfo is null) return NotFound();
            oldProductInfo.Name = productInfo.Name;
            oldProductInfo.Text = productInfo.Text;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            ProductInfo productInfo = _context.ProductInfos.FirstOrDefault(c => c.Id == id);
            if (productInfo is null) return NotFound();
            return View(productInfo);
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 1) return BadRequest();
            ProductInfo productInfo = _context.ProductInfos.FirstOrDefault(c => c.Id == id);
            if (productInfo is null) return NotFound();
            if (productInfo.IsDeleted) productInfo.IsDeleted = false;
            else productInfo.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


     
    }
}
