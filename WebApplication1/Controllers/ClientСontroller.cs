using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientController : Controller
    {
        private DBConector db;
        public ClientController(DBConector db)
        {
            this.db = db;            
        }
        // GET: ClientСontroller
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Index()
        {
            var res = await db.Clients.Include(t => t.Tickets).OrderBy(t => t.Id).ToListAsync();
            ViewData["Title"] = "Клиенты";
            return View(res);
        }


        // GET: ClientСontroller/Create
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление клиента";
            Client client = new Client();
            return View(client);
        }

        // POST: ClientСontroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Add(client);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Добавление клиента";
            return View(client);
        }

        // GET: ClientСontroller/Edit/5
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Edit(int id)
        {
            Client client = await db.Clients.Include(t => t.Tickets).Where(t => t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение клиента";
            return View(client);
        }

        // POST: ClientСontroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение клиента";
            return View(client);
        }


        public async Task<IActionResult> CheckPass(int? Id, string PassNumber)
        {
            if (Id != null)
            {
                var res1 = await db.Clients.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
                var res2 = await db.Clients.Where(t => t.PassNumber == PassNumber).Select(t => t).FirstOrDefaultAsync();
                if (res2 == null || res1.Id == res2?.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else
            {
                var res3 = await db.Clients.Where(t => t.PassNumber == PassNumber).Select(t => t).FirstOrDefaultAsync();
                if (res3 != null)
                    return Json(false);
                return Json(true);
            }
        }


        // GET: ClientСontroller/Delete/5
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Clients.Include(t => t.Tickets).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null && res.Tickets?.Count == 0)
            {
                db.Clients.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
