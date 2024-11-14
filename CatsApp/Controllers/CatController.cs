using CatsApp.Data;
using CatsApp.Data.Models;
using CatsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatsApp.Controllers
{
    public class CatController : Controller
    {
        private readonly ApplicationDbContext db;
        public CatController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CatViewModel model)
        {
            Cat cat = new Cat
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };
            db.Cats.Add(cat);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult Details(int id)
        {
            Cat cat = db.Cats.FirstOrDefault(c => c.Id == id);
            CatViewModel model = new CatViewModel
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };
            return View(model);
        }
        public IActionResult All()
        {
            List<CatViewModel> model = db.Cats.Select(x=> 
            new CatViewModel 
            { 
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Breed = x.Breed,
                ImageUrl = x.ImageUrl
            }).ToList();
            return View(model); 
        }
        public IActionResult Edit(int id) //update
        {
            Cat cat = db.Cats.FirstOrDefault(x => x.Id == id);
            CatViewModel model = new CatViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CatViewModel model)
        {
            Cat cat = db.Cats.FirstOrDefault(x => x.Id == model.Id);
            cat.Name = model.Name;
            cat.Age = model.Age;
            cat.Breed = model.Breed;
            cat.ImageUrl = model.ImageUrl;
            db.SaveChanges();
            return Redirect("/Cat/All");
        }
        public IActionResult Delete(int id)
        {
            Cat cat = db.Cats.FirstOrDefault(x=>x.Id == id);
            db.Cats.Remove(cat);
            db.SaveChanges();
            return Redirect("/Cat/All");
        }
    }
}
