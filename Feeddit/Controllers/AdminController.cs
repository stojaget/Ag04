using Feeddit.DAL;
using Feeddit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feeddit.Controllers
{
    public class AdminController : Controller
    {
       
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Registration(User objNewUser)
        {
            try
            {
                using (var context = new FeedditContext())
                {
                    var chkUser = (from s in context.Users where s.UserName == objNewUser.UserName select s).FirstOrDefault();
                    if (chkUser == null)
                    {
                        var keyNew = Helper.Registration.GeneratePassword(10);
                        var password = Helper.Registration.EncodePassword(objNewUser.Password, keyNew);
                        objNewUser.Password = password;
                       
                        context.Users.Add(objNewUser);
                        context.SaveChanges();
                        ModelState.Clear();
                        return RedirectToAction("Login", "Login");
                    }
                    ViewBag.ErrorMessage = "Korisnik već postoji u sustavu.";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Dogodila se pogreška u sustavu. Molimo pokušajte ponovo." + e;
                return View();
            }
        }
    }
}