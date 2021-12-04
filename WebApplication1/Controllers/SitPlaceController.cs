//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    public class SitPlaceController : Controller
//    {
//        //private DBConector db;
//        //public SitPlaceController(DBConector db)
//        //{
//        //    this.db = db;
//        //}
//        //// GET: SitPlaceController
//        //public async Task<IActionResult> Index()
//        //{
//        //    //
//        //    var res = await db.SitPlaces.Include(t => t.Vans).Include(t=>t.Van_SitPlaces).ToListAsync();            
//        //    ViewData["Title"] = "Места";
//        //    return View(res);
//        //}


//        //// GET: SitPlaceController/Create
//        //public async Task<IActionResult> Create()
//        //{
//        //    ViewData["Title"] = "Добавление места";           
//        //    SitPlace sitPlace = new SitPlace();
//        //    return View(sitPlace);
//        //}

//        //// POST: SitPlaceController/Create
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create(SitPlace sitPlace)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Add(sitPlace);

//        //        await db.SaveChangesAsync();
//        //        return RedirectToAction("Index");
//        //    }
//        //    ViewData["Title"] = "Добавление места";
//        //    return View(sitPlace);
//        //}

//        //// GET: SitPlaceController/Edit/5
//        //public async Task<IActionResult> Edit(int id)
//        //{
//        //    SitPlace sitPlace = await db.SitPlaces.Include(t => t.Vans).Include(t => t.Van_SitPlaces).Where(t => t.Id == id).FirstOrDefaultAsync();
//        //    ViewData["Title"] = "Изменение места";
//        //    return View(sitPlace);
//        //}

//        //// POST: SitPlaceController/Edit/5
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Edit(SitPlace sitPlace)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Entry(sitPlace).State = EntityState.Modified;
//        //        await db.SaveChangesAsync();
//        //        return RedirectToAction("Index");
//        //    }
//        //    ViewData["Title"] = "Изменение места"; 
//        //    return View(sitPlace);
//        //}

//        //public async Task<IActionResult> CheckName(int? Id, string Name)
//        //{
//        //    if (Id != null)
//        //    {
//        //        var res1 = await db.SitPlaces.Where(t => t.Id == Id).Select(t => t).FirstOrDefaultAsync();
//        //        var res2 = await db.SitPlaces.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
//        //        if (res2 == null || res1.Id == res2?.Id)
//        //        {
//        //            return Json(true);
//        //        }
//        //        return Json(false);
//        //    }
//        //    else
//        //    {
//        //        var res3 = await db.SitPlaces.Where(t => t.Name == Name).Select(t => t).FirstOrDefaultAsync();
//        //        if (res3 != null)
//        //            return Json(false);
//        //        return Json(true);
//        //    }
//        //}


//        //// GET: SitPlaceController/Delete/5
//        //public async Task<IActionResult> Delete(int id)
//        //{
//        //    var res = await db.SitPlaces.Include(t => t.Vans).Include(t => t.Van_SitPlaces).Where(t => t.Id == id).FirstOrDefaultAsync();

//        //    if (res != null && res.Vans?.Count == 0 && res.Van_SitPlaces?.Count == 0)
//        //    {
//        //        db.SitPlaces.Remove(res);
//        //        await db.SaveChangesAsync();                
//        //    }
//        //    return RedirectToAction("Index");
//        //}
//    }
//}
