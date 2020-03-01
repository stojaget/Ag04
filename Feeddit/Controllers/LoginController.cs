using Feeddit.DAL;
using Feeddit.Helper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feeddit.Controllers
{
    public class LoginController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Feeddit.Models.User userModel)
        {
            
            using (FeedditContext db = new FeedditContext())
            {
                if (userModel.UserName == "" || userModel.Password == "")
                {
                    TempData["alert"] = "Lozinka i korisničko ime su obavezna polja.";
                    TempData["aType"] = "danger";
                }
         
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    TempData["alert"] = "Korisničko ime ili lozinka su neispravni.";
                    TempData["aType"] = "danger";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.ID;
                    Session["userName"] = userDetails.UserName;
                    Session["Rola"] = userDetails.Role;
                    logger.Info("User " + userDetails.UserName + " log in, date: " + DateTime.Now);
                    return RedirectToAction("Index", "Articles");
                }
            }
        }

        public ActionResult LogOut()
        {
            int UserID = (int)Session["UserID"];
            Session.Abandon();
            TempData["alert"] = "Uspješno ste se odjavili.";
            TempData["aType"] = "success";
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}