using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication1.Models;
using X.PagedList;

namespace WebApplication1.Controllers
{
    public class BikeController : Controller
    {

        private readonly MyDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        [BindProperty]
        public BikeVM  bikeVM { get; set; }
        public BikeController(MyDbContext db , IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
            bikeVM = new BikeVM
            { 
                Makes = db.Makes.ToList(),
                Models = db.Models.ToList(),  Bike = new Bike()
            };
        }

        public IActionResult Index(string SearchString ,string sortOrder,  int ? Page)
        {
            ViewBag.priceSortParm =  string.IsNullOrEmpty(sortOrder) ? "PriceDes" : "";  /// short if 

            var mod =new List<Bike>();

            //var model = db.Bikes.Include(x => x.Make).Include(x=>x.Model).OrderByDescending(x=>x.Price);

            if (SearchString != null || !string.IsNullOrEmpty(SearchString))
            {
                mod=db.Bikes.Where(x => x.Make.Name.Contains(SearchString)).Include(x => x.Make).Include(x => x.Model).ToList() ;
               // model.Where(x => x.Make.Name.Contains(SearchString));
                return View(mod.ToPagedList(Page ?? 1, 3));
            }
            if (sortOrder == "PriceDes")
            {
                //model.OrderByDescending(x => x.Price);
                mod = db.Bikes.Include(x => x.Make).Include(x => x.Model).OrderByDescending(x => x.Price).ToList();

                //mod.OrderByDescending(x => x.Price).ToList();
                return View(mod.ToPagedList(Page ?? 1, 3));
            }
           mod = db.Bikes.Include(x => x.Make).Include(x => x.Model).ToList();

            return View(mod.ToPagedList(Page ?? 1, 3) );
        }


        [HttpGet]
        public IActionResult Create()
        {

            ViewData["AllAuthor"] = new SelectList(db.Makes.ToList(), "Id", "Name");
            ViewData["AllAuthor2"] = new SelectList(db.Models.ToList(), "Id", "Name");
            ViewData["CureencyList"]= new SelectList( bikeVM.Currencies.ToList() ,"Id","Name" );

            return View(bikeVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult PostCreate()
        {
            var fileName = string.Empty;
       
            if (ModelState.IsValid)
            {

                if (bikeVM.File != null)
                {
                    var upload = Path.Combine(hostingEnvironment.WebRootPath, "Images"); /// to combine wwwRoot and folder

                    fileName = Guid.NewGuid().ToString() + "_" + bikeVM.File.FileName;
                    var fullPath = Path.Combine(upload, fileName);

                    bikeVM.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                
                bikeVM.Bike.ImgURL = fileName;


                db.Bikes.Add(bikeVM.Bike);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }

            ViewData["AllAuthor"] = new SelectList(db.Makes.ToList(), "Id", "Name" );
            ViewData["AllAuthor2"] = new SelectList(db.Models.ToList(), "Id", "Name");
            ViewData["CureencyList"] = new SelectList(bikeVM.Currencies.ToList(), "Id", "Name");
            return View(bikeVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            bikeVM.Bike = db.Bikes.Where(x => x.Id==id).FirstOrDefault();

            // filter the model to selected item
           // bikeVM.Models = db.Models.Where(x => x.MakeID == bikeVM.Bike.MakeID);


            ViewData["AllAuthor"] = new SelectList(db.Makes.ToList(), "Id", "Name", bikeVM.Bike.MakeID);
            ViewData["AllAuthor2"] = new SelectList(db.Models.ToList(), "Id", "Name",bikeVM.Bike.ModelID);
             
            return View(bikeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult PostEdit()
        {
            if (ModelState.IsValid)
            {
                db.Bikes.Update(bikeVM.Bike);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AllAuthor"] = new SelectList(db.Makes.ToList(), "Id", "Name", bikeVM.Bike.MakeID);
            ViewData["AllAuthor2"] = new SelectList(db.Models.ToList(), "Id", "Name", bikeVM.Bike.ModelID);
           
            return View(bikeVM);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var val = db.Bikes.Find(id);
            db.Bikes.Remove(val);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }




    }
}
