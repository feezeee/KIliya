using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TrainController : Controller
    {
        private DBConector db;
        public TrainController(DBConector db)
        {
            this.db = db;
        }
        // GET: TrainController
        [Authorize(Roles = "Администратор, Кассир")]
        public async Task<IActionResult> Index()
        {
            //
            var res = await db.Trains.Include(t => t.TrainDestinations).ToListAsync();            
            ViewData["Title"] = "Поезда";
            return View(res);
        }


        // GET: TrainController/Create
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление поезда";           
            Train train = new Train();
            return View(train);
        }

        // POST: TrainController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create(Train train)
        {
            if (ModelState.IsValid)
            {
                db.Add(train);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление поезда";
            return View(train);
        }

        // GET: TrainController/Edit/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(int id)
        {
            Train train = await db.Trains.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение поезда";            
            return View(train);
        }

        // POST: TrainController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(Train train)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение поезда";
            return View(train);
        }

        //public async Task<IActionResult> CheckName(int? Id, string Name)
        //{
        //    if (Id != null)
        //    {
        //        var res1 = await db.Trains.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
        //        var res2 = await db.Trains.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
        //        if (res2 == null || res1.Id == res2?.Id)
        //        {
        //            return Json(true);
        //        }
        //        return Json(false);
        //    }
        //    else
        //    {
        //        var res3 = await db.Trains.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
        //        if (res3 != null)
        //            return Json(false);
        //        return Json(true);
        //    }
        //}


        // GET: TrainController/Delete/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Trains.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null && res.TrainDestinations?.Count == 0)
            {
                db.Trains.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
