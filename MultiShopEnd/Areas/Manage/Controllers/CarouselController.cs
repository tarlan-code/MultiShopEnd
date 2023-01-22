using Microsoft.AspNetCore.Mvc;
using MultiShopEnd.DAL;
using MultiShopEnd.Extensions;
using MultiShopEnd.Models;
using MultiShopEnd.ViewModels;

namespace MultiShopEnd.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class CarouselController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public CarouselController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.CarouselItems.OrderBy(c=>c.Order));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            CarouselItem carousel = _context.CarouselItems.FirstOrDefault(c => c.Id == id);
            if(carousel is null) return NotFound();
            carousel.ImageUrl.DeleteFile(_env.WebRootPath, Path.Combine("assets", "img", "Carousel"));
            _context.CarouselItems.Remove(carousel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarouselVM carousel)
        {
            if (carousel is null) return BadRequest();

            IFormFile image = carousel.Image;
            string result = image?.CheckValidation(300,"image") ?? "";
            if (result.Length>0) ModelState.AddModelError("Image", result);


            if(_context.CarouselItems.Any(p=>p.Order == carousel.Order)) ModelState.AddModelError("Order", $"There is a carousel in {carousel.Order}");
            
            if (!ModelState.IsValid) return View();
            CarouselItem newCarousel = new CarouselItem
            {
                Title = carousel.Title,
                Desc = carousel.Desc,
                BtnText = carousel.BtnText,
                BtnUrl = carousel.BtnUrl,
                Order = carousel.Order,
            };

            newCarousel.ImageUrl =  image.SaveFile(Path.Combine(_env.WebRootPath,"assets","img","Carousel"));

            _context.CarouselItems.Add(newCarousel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();



            CarouselItem carousel = _context.CarouselItems.FirstOrDefault(c => c.Id == id);
            if (carousel is null) return NotFound();



            UpdateCarouselVM carouselVM = new UpdateCarouselVM
            {
                Title = carousel.Title,
                Desc = carousel.Desc,
                BtnText = carousel.BtnText,
                BtnUrl = carousel.BtnUrl,
                Order = carousel.Order,
                ImageUrl = carousel.ImageUrl,
            };
            return View(carouselVM);
        }


        [HttpPost]
        public IActionResult Update(int? id,UpdateCarouselVM carousel)
        {

            if(id is null || id<=0 || carousel is null) return BadRequest();


            IFormFile image = carousel.Image;
            if(image is not null)
            {
                string result = image?.CheckValidation(300, "image") ?? "";
                if (result.Length > 0) ModelState.AddModelError("Image", result);
            }
            CarouselItem oldCarousel = _context.CarouselItems.FirstOrDefault(c => c.Id == id);
            if (oldCarousel is null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.ImgUrl = oldCarousel.ImageUrl;
                return View();
            }
            
            oldCarousel.Title = carousel.Title;
            oldCarousel.Desc = carousel.Desc;
            oldCarousel.BtnText = carousel.BtnText;
            oldCarousel.BtnUrl = carousel.BtnUrl;

            CarouselItem sameOrder = _context.CarouselItems.FirstOrDefault(c => c.Order == carousel.Order);
            if (sameOrder is not null) sameOrder.Order = oldCarousel.Order;

            oldCarousel.Order = carousel.Order;

            if (image is not null)
                oldCarousel.ImageUrl = image.SaveFileWithName(Path.Combine(_env.WebRootPath, "assets", "img", "Carousel"),oldCarousel.ImageUrl);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Detail(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            CarouselItem carousel = _context.CarouselItems.FirstOrDefault(c => c.Id == id);
            if (carousel is null) return NotFound();


            return View(carousel);
        }

        public IActionResult ChangeStatus(int? id)
        {
            if (id is null && id <= 0) return BadRequest();
            CarouselItem carousel = _context.CarouselItems.FirstOrDefault(c => c.Id == id);
            if(carousel is null) return NotFound();
            if (carousel.IsDeleted) carousel.IsDeleted = false;
            else carousel.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
