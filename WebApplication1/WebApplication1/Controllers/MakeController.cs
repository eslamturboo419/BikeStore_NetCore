using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MakeController : Controller
    {
        private readonly MyDbContext db;

        public MakeController(MyDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var val = db.Makes.ToList();

            return View(val);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Add(make);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(make);
        }


        [HttpGet]
        public IActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var val = db.Makes.Find(id);
            return View(val);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Update(make);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }




        public IActionResult Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var val = db.Makes.Find(id);
            db.Makes.Remove(val);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
