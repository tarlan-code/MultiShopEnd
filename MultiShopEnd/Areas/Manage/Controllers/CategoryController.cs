using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Extensions;
using MultiShopEnd.Models;
using MultiShopEnd.ViewModels;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class CategoryController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Categories.Where(c=>c.Id != 3));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();
            category.ImageUrl.DeleteFile(_env.WebRootPath, Path.Combine("assets", "img", "Category"));
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryVM category)
        {
            if (category is null) return BadRequest();

            IFormFile image = category.Image;
            string result = image?.CheckValidation(300, "image") ?? "";
            if (result.Length > 0) ModelState.AddModelError("Image", result);


            if (!ModelState.IsValid) return View();
            Category newCategory = new Category
            {
                Name = category.Name
            };

            newCategory.ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "Category"));

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();



            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();



            UpdateCategoryVM categoryVM = new UpdateCategoryVM
            {
                Name = category.Name,
                ImageUrl = category.ImageUrl,
            };
            return View(categoryVM);
        }


        [HttpPost]
        public IActionResult Update(int? id, UpdateCategoryVM category)
        {

            if (id is null || id <= 0 || category is null) return BadRequest();


            IFormFile image = category.Image;
            if (image is not null)
            {
                string result = image?.CheckValidation(300, "image") ?? "";
                if (result.Length > 0) ModelState.AddModelError("Image", result);
            }
            Category oldCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (oldCategory is null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.ImgUrl = oldCategory.ImageUrl;
                return View();
            }


            oldCategory.Name= category.Name;


            if (image is not null)
                oldCategory.ImageUrl = image.SaveFileWithName(Path.Combine(_env.WebRootPath, "assets", "img", "Category"), oldCategory.ImageUrl);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Detail(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();

            return View(category);
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 0) return BadRequest();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null) return NotFound();
            if (category.IsDeleted) category.IsDeleted = false;
            else category.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
