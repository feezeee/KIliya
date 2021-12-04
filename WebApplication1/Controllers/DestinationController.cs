using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DestinationController : Controller
    {
        private DBConector db;
        public DestinationController(DBConector db)
        {
            this.db = db;
        }
        // GET: DestinationController
        public async Task<IActionResult> Index()
        {
            //
            var res = await db.Destinations.Include(t => t.TrainDestinations).ToListAsync();            
            ViewData["Title"] = "Места назначения";
            return View(res);
        }


        // GET: DestinationController/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление места назначения";           
            Destination destination = new Destination();
            return View(destination);
        }

        // POST: DestinationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Add(destination);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление места назначения";
            return View(destination);
        }

        // GET: DestinationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Destination destination = await db.Destinations.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение места назначения";            
            return View(destination);
        }

        // POST: DestinationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение места назначения";
            return View(destination);
        }

        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await db.Destinations.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await db.Destinations.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await db.Destinations.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        // GET: DestinationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Destinations.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();

            if(res != null && res.TrainDestinations?.Count == 0)
            {
                db.Destinations.Remove(res);
                await db.SaveChangesAsync();                
            }
            return RedirectToAction("Index");
        }
    }
}
