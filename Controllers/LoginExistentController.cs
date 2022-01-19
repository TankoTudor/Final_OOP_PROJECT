using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Final_OOP_PROJECT.Models;

namespace Final_OOP_PROJECT.Controllers
{
    public class LoginExistentController : Controller
    {
        private IndividDBContext iDb = new IndividDBContext();

        // GET: LoginExistent
        public ActionResult LoginExistent()
        {
            return View();//iDb.User.ToList()
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginExistent(Individ objUser)
        {
            if (ModelState.IsValid)
            {
                using (iDb)
                {
                    var obj = iDb.User.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.IdUser.ToString();
                        Session["UserName"] = obj.Username.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Individ msg)
        {
            if (ModelState.IsValid)
            {
                iDb.User.Add(msg);
                iDb.SaveChanges();

                return RedirectToAction("LoginExistent");
            }
            return View(msg);
        }
    }
}