using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccessRightController : Controller
    {
        private DBConector db;
        public AccessRightController(DBConector db)
        {
            this.db = db;
        }
        // GET: AccessRightController
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Index()
        {
            var res = await db.AccessRights.Include(t => t.Workers).ToListAsync();            
            ViewData["Title"] = "Группа пользователей";
            return View(res);
        }


        // GET: AccessRightController/Create
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление группы пользователей";
            AccessRight accessRight = new AccessRight();
            return View(accessRight);
        }

        // POST: AccessRightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create(AccessRight accessRight)
        {
            if (ModelState.IsValid)
            {
                db.Add(accessRight);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление группы пользователей";
            return View(accessRight);
        }

        // GET: AccessRightController/Edit/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Изменение группы пользователей";
            AccessRight accessRight = await db.AccessRights.Include(t => t.Workers).Where(t => t.Id == id).FirstOrDefaultAsync();
            return View(accessRight);
        }

        // POST: AccessRightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(AccessRight accessRight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessRight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение группы пользователей";
            return View(accessRight);
        }


        public async Task<IActionResult> CheckName(int? Id, string Name)
        {
            if (Id != null)
            {
                var res1 = await db.AccessRights.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await db.AccessRights.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await db.AccessRights.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        // GET: AccessRightController/Delete/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.AccessRights.Include(t => t.Workers).Where(t => t.Id == id).FirstOrDefaultAsync();

            if(res != null && res.Workers?.Count == 0)
            {
                db.AccessRights.Remove(res);
                await db.SaveChangesAsync();                
            }
            return RedirectToAction("Index");
        }

    }
}
