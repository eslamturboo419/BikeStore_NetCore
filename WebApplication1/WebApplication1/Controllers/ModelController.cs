using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ModelController : Controller
    {
        private readonly MyDbContext db;

        [BindProperty]
        public ModelVM ModelVM  { get; set; }
        public ModelController(MyDbContext db)
        {
            this.db = db;
            ModelVM = new ModelVM
            {
                makes = db.Makes.ToList(),
                model = new Model()
            };
        }

        public IActionResult Index()
        {
            var model = db.Models.Include(x=>x.Make);

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(ModelVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult PostCreate()
        {
            if (ModelState.IsValid)
            {
                db.Models.Add(ModelVM.model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(ModelVM);
        }

        [HttpGet]
        public IActionResult Edit(int ?id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ModelVM.model = db.Models.Include(x => x.Make).FirstOrDefault();

            return View(ModelVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult PostEdit()
        {
            if (ModelState.IsValid)
            {
                db.Models.Update(ModelVM.model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(ModelVM);
        }


        public IActionResult Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var val = db.Models.Find(id);
            db.Models.Remove(val);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
                 
        }






        /// API Method

        [HttpGet("api/models/{MakeID}")]
        public IEnumerable<Model> Models(int MakeID)
        {
            var val = db.Models.Where(x=>x.MakeID== MakeID).ToList() ;
            return val;
        }

    }
}
