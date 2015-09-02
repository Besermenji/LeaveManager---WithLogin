using LeaveManager___WithLogin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManager___WithLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        [ChildActionOnly]
        public ViewResult _LoginPartial()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.Name = user.FirstName;
            ViewData["name"] = user.Name;

            return View("~/Views/Shared/_LoginPartial");
        }
    }
}