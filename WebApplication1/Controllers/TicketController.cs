using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TicketController : Controller
    {
        private DBConector db;
        public TicketController(DBConector db)
        {
            this.db = db;
        }
        // GET: TicketController
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Index()
        {
            //
            var res = await db.Tickets.Include(t=>t.Client).Include(t=>t.Worker).Include(t=>t.TrainVanSit).ThenInclude(t=>t.Train).Include(t => t.TrainVanSit).ThenInclude(t => t.Van).Include(t => t.TrainVanSit).ThenInclude(t => t.SitPlace).ToListAsync();
            ViewData["Title"] = "Билеты";
            return View(res);
        }


        // GET: TicketController/Create
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Добавление билета";

            List<SelectListItem> trainVanSits = new List<SelectListItem>();
            foreach (var el in await db.TrainVanSits.Include(t=>t.Train).Include(t=>t.Van).Include(t=>t.SitPlace).Include(t=>t.Ticket).Where(t=>t.Ticket.Count == 0).ToListAsync())
            {
                trainVanSits.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.TrainVanSitId.ToString() });
            }

            ViewBag.TrainVanSits = trainVanSits;


            List<SelectListItem> clients = new List<SelectListItem>();
            foreach (var el in await db.Clients.OrderBy(t => t.Id).ToListAsync())
            {
                clients.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.Id.ToString() });
            }


            ViewBag.Clients = clients;
            Ticket ticket = new Ticket();
            return View(ticket);
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор, Кассир")]

        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var worker = AuthorizedUser.GetInstance().GetWorker();
                
                if(worker != null)
                {   ticket.WorkerId = worker.Id;
                    db.Add(ticket);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }                
            }

            List<SelectListItem> trainVanSits = new List<SelectListItem>();

            foreach (var el in await db.TrainVanSits.Include(t => t.Train).Include(t => t.Van).Include(t => t.SitPlace).Include(t => t.Ticket).Where(t => t.Ticket.Count == 0).ToListAsync())
            {
                trainVanSits.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.TrainVanSitId.ToString() });
            }

            ViewBag.TrainVanSits = trainVanSits;


            List<SelectListItem> clients = new List<SelectListItem>();
            foreach (var el in await db.Clients.OrderBy(t => t.Id).ToListAsync())
            {
                clients.Add(new SelectListItem { Text = $"{el.GetInfo()}", Value = el.Id.ToString() });
            }


            ViewBag.Clients = clients;

            ViewData["Title"] = "Добавление билета";
            return View(ticket);
        }


        // GET: TicketController/Delete/5
        [Authorize(Roles = "Администратор, Кассир")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await db.Tickets.Include(t => t.Client).Include(t => t.Worker).Where(t => t.Id == id).FirstOrDefaultAsync();

            if (res != null)
            {
                db.Tickets.Remove(res);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
