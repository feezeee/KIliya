using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SitPlaceController : Controller
    {
        private DBConector db;
        public SitPlaceController(DBConector db)
        {
            this.db = db;
        }
        // GET: SitPlaceController
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Index()
        {
            var res = await db.SitPlaces.FromSqlRaw("GetSit_places").ToListAsync();
            foreach(var el in res)
            {
                el.TrainVanSits = await db.TrainVanSits.Where(t=>t.SitPlaceId == el.Id).ToListAsync();
            }
            ViewData["Title"] = "Места";
            return View(res);
        }


        // GET: SitPlaceController/Create
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление места";
            SitPlace sitPlace = new SitPlace();
            return View(sitPlace);
        }

        // POST: SitPlaceController/Create
        [Authorize(Roles = "Администратор")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SitPlace sitPlace)
        {
            if (ModelState.IsValid)
            {
                db.Add(sitPlace);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление места";
            return View(sitPlace);
        }

        // GET: SitPlaceController/Edit/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(int id)
        {
            SitPlace sitPlace = await db.SitPlaces.Include(t=>t.TrainVanSits).Where(t => t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение места";
            return View(sitPlace);
        }

        // POST: SitPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(SitPlace sitPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sitPlace).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение места";
            return View(sitPlace);
        }

        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await db.SitPlaces.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await db.SitPlaces.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await db.SitPlaces.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        // GET: SitPlaceController/Delete/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.SitPlaces.Include(t => t.TrainVanSits).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null && res.TrainVanSits?.Count == 0)
            {
                db.SitPlaces.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
