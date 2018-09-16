using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        readonly IStringLocalizer<AccountController> localizer;
        readonly ILogger logger;
        queueDBContext db;
        Queue queue;

        public AccountController(queueDBContext context, IStringLocalizer<AccountController> _localizer,
                                                                   ILogger<AccountController> _logger)
        {
            db = context;
            queue = new Queue(db);
            localizer = _localizer;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                    if (user != null)
                    {
                        await Authenticate(model.Email);
                        ViewBag.Status = queue.GetStatusMicrowave();
                        TempData["UserLogin"] = user.Email;
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["message"] = localizer["Incorrect"];
                }
                return View(model);
            }
            catch (SqlException ex)
            {
                TempData["message"] = localizer["ConnectToDataBase"];
                logger.LogError(ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View();
            }           
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        db.User.Add(new User { Name = model.Name, Email = model.Email, Password = model.Password });
                        await db.SaveChangesAsync();

                        await Authenticate(model.Email);

                        ViewBag.Status = queue.GetStatusMicrowave();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        TempData["message"] = localizer["Exists"];
                }
                return View(model);
            }
            catch (SqlException ex)
            {
                TempData["message"] = localizer["ConnectToDataBase"];
                logger.LogError(ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    TempData["message"] = localizer["Exists"];
                }
                logger.LogError(ex.Message);
                return View();
            }           
        }

        [HttpGet]
        public IActionResult SettingAccount()
        {
            try
            {
                User user = db.User.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    RegisterModel model = new RegisterModel()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = "",
                        ConfirmPassword = ""
                    };
                    return View(model);
                }
                return View();
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return View();
            }           
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SettingAccount(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user != null)
                    {
                        user.Name = model.Name;
                        user.Email = model.Email;
                        user.Password = model.Password;
                        await db.SaveChangesAsync();
                        await Authenticate(model.Email);

                        ViewBag.Status = queue.GetStatusMicrowave();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        TempData["message"] = localizer["Incorrect"];
                }
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return View();
            }
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}