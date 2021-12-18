using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class WorkerController : Controller
    {
        private DBConector db;
        public WorkerController(DBConector db)
        {
            this.db = db;
            //Worker worker = db.Workers.Include(t => t.AccessRight).Include(t => t.Tickets).Include(t => t.Tickets).First();
            //AuthorizedUser.GetInstance().SetUser(worker);
        }
        // GET: WorkerController
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Index()
        {
            var res = await db.Workers.FromSqlRaw("GetWorkers").ToListAsync();
            foreach(var el in res)
            {
                el.AccessRight = db.AccessRights.Find(el.AccessRightId);
                el.Tickets = await db.Tickets.Where(t => t.WorkerId == el.Id).ToListAsync();
            }            
            ViewData["Title"] = "Сотрудники";
            return View(res);
        }


        // GET: WorkerController/Create
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление сотрудника";
            ViewBag.AccessRights = new SelectList(await db.AccessRights.OrderBy(t => t.Id).ToListAsync(), "Id", "Name");
            Worker worker = new Worker();
            return View(worker);
        }

        // POST: WorkerController/Create
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Add(worker);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AccessRights = new SelectList(await db.AccessRights.OrderBy(t=>t.Id).ToListAsync(), "Id", "Name");
            ViewData["Title"] = "Добавление сотрудника";
            return View(worker);
        }

        // GET: WorkerController/Edit/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int id)
        {
            Worker worker = await db.Workers.Include(t => t.AccessRight).Include(t => t.Tickets).Include(t => t.Tickets).Where(t=>t.Id == id).FirstOrDefaultAsync();
            ViewData["Title"] = "Изменение сотрудника";
            ViewBag.AccessRights = new SelectList(await db.AccessRights.OrderBy(t => t.Id).ToListAsync(), "Id", "Name");
            return View(worker);
        }

        // POST: WorkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Edit(Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Title"] = "Изменение сотрудника";
            ViewBag.AccessRights = new SelectList(await db.AccessRights.OrderBy(t => t.Id).ToListAsync(), "Id", "Name");
            return View(worker);
        }


        // GET: WorkerController/Delete/5
        [Authorize(Roles = "Администратор")]

        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Workers.Include(t => t.AccessRight).Include(t=>t.Tickets).Include(t => t.Tickets).Where(t=>t.Id == id).FirstOrDefaultAsync();

            if(res != null && res.Id != AuthorizedUser.GetInstance()?.GetWorker()?.Id)
            {
                db.Workers.Remove(res);
                await db.SaveChangesAsync();                
            }
            return RedirectToAction("Index");
        }

    }
}
