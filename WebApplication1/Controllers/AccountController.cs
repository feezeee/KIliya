using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private DBConector db;
        public AccountController(DBConector db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                Worker user = await db.Workers
                    .Include(t => t.AccessRight)
                    .Where(t => t.PhoneNumber == model.PhoneNumber && t.Password == model.Password)
                    .FirstOrDefaultAsync();

                if (user != null)
                {

                    await Authenticate(user); // аутентификация
                    AuthorizedUser.GetInstance().ClearUser();
                    AuthorizedUser.GetInstance().SetUser(user);
                    return RedirectToAction("Index", "Home");
                    
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await HttpContext.SignOutAsync();
            AuthorizedUser.GetInstance().ClearUser();
            return RedirectToAction("Login", "Account");
        }
        private async Task Authenticate(Worker worker)
        {
            // создаем один claim
            var claims = new List<Claim>
            {                
                new Claim(ClaimsIdentity.DefaultNameClaimType, worker.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, worker?.AccessRight?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
