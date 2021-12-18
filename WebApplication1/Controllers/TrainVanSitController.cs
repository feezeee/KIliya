using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TrainVanSitController : Controller
    {
        private DBConector db;
        public TrainVanSitController(DBConector db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Администратор, Кассир")]
        public async Task<IActionResult> Index()
        {
            var res = await db.TrainVanSits.Include(t => t.Train).Include(t => t.SitPlace).Include(t => t.Van).Include(t=>t.Ticket).OrderBy(t => t.SitPlaceId).OrderBy(t => t.VanId).OrderBy(t => t.TrainId).ToListAsync();
            return View(res);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create()
        {
            //ViewBag.Trains = new SelectList(await db.Trains.ToListAsync(), "Id", "Name");

            //Student student = _context.Students.Include(t => t.Class).ThenInclude(t => t.ClassChar).Include(t => t.Class).ThenInclude(t => t.ClassType).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            
            List<SelectListItem> trains = new List<SelectListItem>();
            foreach (var el in await db.Trains.ToListAsync())
            {
                trains.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.Id.ToString() });
            }

            ViewBag.Trains = trains;   
            ViewBag.Vans = new SelectList(await db.Vans.ToListAsync(), "Id", "Name");
            ViewBag.SitPlaces = await db.SitPlaces.ToListAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create(TrainVanSit trainVanSit, int [] selectedSits)
        {
            if (ModelState.IsValid)
            {
                foreach(var el in selectedSits)
                {
                    trainVanSit.TrainVanSitId = 0;
                    trainVanSit.SitPlaceId = el;
                    db.Add(trainVanSit);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            List<SelectListItem> trains = new List<SelectListItem>();
            foreach (var el in await db.Trains.ToListAsync())
            {
                trains.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.Id.ToString() });
            }

            ViewBag.Trains = trains;
            ViewBag.Vans = new SelectList(await db.Vans.ToListAsync(), "Id", "Name");
            ViewBag.SitPlaces = await db.SitPlaces.ToListAsync();
            return View(trainVanSit);
        }

        // GET: WorkerController/Delete/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.TrainVanSits.Include(t => t.Train).Include(t => t.SitPlace).Include(t => t.Van).Include(t=>t.Ticket).Include(t => t.Ticket).Where(t => t.TrainVanSitId == id).FirstOrDefaultAsync();

            if (res != null)
            {
                db.TrainVanSits.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
