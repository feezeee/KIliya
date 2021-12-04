using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TrainDestinationController : Controller
    {
        private DBConector db;
        public TrainDestinationController(DBConector db)
        {
            this.db = db;
        }
        // GET: TrainDestinationController
        public async Task<IActionResult> Index()
        {
            
            var res = db.TrainDestinations.Include(t => t.Train).Include(t=>t.Destination).OrderBy(t => t.DepartureTime).OrderBy(t=>t.ArrivalTime).AsEnumerable().GroupBy(t => t.TrainId);
            ViewData["Title"] = "Поезда и маршруты";
            return View(res);
        }


        // GET: TrainDestinationController/Create
        public async Task<IActionResult> Create()
        {
            TrainDestination trainDestination = new TrainDestination();
            ViewData["Title"] = "Добавление маршрута к поезду";
            ViewBag.Destinations = new SelectList(await db.Destinations.ToListAsync(), "Id", "Name");
            ViewBag.Trains = new SelectList(await db.Trains.ToListAsync(), "Id", "Name");
            return View(trainDestination);
        }

        // POST: TrainDestinationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainDestination trainDestination)
        {
            if (ModelState.IsValid)
            {
                db.Add(trainDestination);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление маршрута к поезду";
            ViewBag.Destinations = new SelectList(await db.Destinations.ToListAsync(), "Id", "Name");
            ViewBag.Trains = new SelectList(await db.Trains.ToListAsync(), "Id", "Name");
            return View(trainDestination);
        }

        // GET: TrainDestinationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Train train = await db.Trains.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение поезда";
            return View(train);
        }

        // POST: TrainDestinationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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


        // GET: TrainDestinationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.TrainDestinations.Include(t => t.TrainDestinations).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null && res.TrainDestinations?.Count == 0)
            {
                db.Trains.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
