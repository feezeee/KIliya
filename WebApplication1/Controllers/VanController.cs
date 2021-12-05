using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VanController : Controller
    {
        private DBConector db;
        public VanController(DBConector db)
        {
            this.db = db;
        }
        // GET: VanController
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Index()
        {
            var res = await db.Vans.Include(t => t.TrainVanSits).ToListAsync();
            ViewData["Title"] = "Вагоны";
            return View(res);
        }


        // GET: VanController/Create
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление вагона";
            Van van = new Van();
            return View(van);
        }

        // POST: VanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create(Van van)
        {
            if (ModelState.IsValid)
            {
                db.Add(van);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление вагона";
            return View(van);
        }

        // GET: VanController/Edit/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Изменение вагона";
            Van van = await db.Vans.Where(t => t.Id == id).FirstOrDefaultAsync();
            return View(van);
        }

        // POST: VanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(Van van)
        {
            if (ModelState.IsValid)
            {
                db.Entry(van).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение вагона";
            return View(van);
        }


        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await db.Vans.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await db.Vans.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await db.Vans.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        // GET: VanController/Delete/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Vans.Include(t => t.TrainVanSits).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null && res.TrainVanSits?.Count == 0)
            {
                db.Vans.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
