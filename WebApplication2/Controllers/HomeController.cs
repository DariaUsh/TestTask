using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        readonly IStringLocalizer<HomeController> localizer;
        readonly ILogger logger;
        queueDBContext db;
        Queue queue;

        public HomeController(queueDBContext context, IStringLocalizer<HomeController> _localizer, 
                                                                   ILogger<HomeController> _logger)
        {
            db = context;
            queue = new Queue(db);
            localizer = _localizer;
            logger = _logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                if (queue.IsInQueue(""))
                {
                    TempData["message"] = localizer["NumberOfQueue"] + queue.numberUser;
                }
                ViewBag.Status = queue.GetStatusMicrowave();
                return View();
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Index");
            }           
        }

        public IActionResult AddInLine()
        {
            try
            {
                if (queue.IsUseMicrowave(User.Identity.Name))
                {
                    TempData["message"] = localizer["UseMicrowave"] + queue.numberMicrowave
                                        + ", " + localizer["RelaxRum" + queue.relaxRumId] 
                                        + ". " + localizer["FinishUseMicrowave"];
                }else if (queue.AddInQueue(User.Identity.Name))
                {
                    TempData["message"] = localizer["NumberOfQueue"] + queue.numberUser;
                }
                else
                {
                    TempData["message"] = localizer["UseMicrowave"] + queue.numberMicrowave
                                        + ", " + localizer["RelaxRum" + queue.relaxRumId]
                                        + ". " + localizer["FinishUseMicrowave"];
                }
                ViewBag.Status = queue.GetStatusMicrowave();
                return View("Index");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Index");
            }           
        }

        public PartialViewResult RemoveInLine()
        {
            try
            {
                if(queue.RemoveInQueue(User.Identity.Name))
                {
                    TempData["message"] = localizer["RemoveList"];
                }
                return PartialView("_Message");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return PartialView("_Message");
            }
        }

        public IActionResult Line()
        {
            try
            {
                var Line = db.Line.Include(c => c.User).ToList();
                ViewBag.Line = Line;
                return View("Line");
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return PartialView("Line");
            }
        }

        public IActionResult Complete()
        {
            try
            {
                if(queue.Complete(User.Identity.Name))
                {
                    TempData["message"] = localizer["ServiceComplete"];
                }
                ViewBag.Status = queue.GetStatusMicrowave();
                return View("Index");
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Index");
            }
        }

        public IActionResult UpdateStatus()
        {
            try
            {
                ViewBag.Status = queue.GetStatusMicrowave();
                return View("Index");
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return View("Index");
            }            
        }
    }
}
