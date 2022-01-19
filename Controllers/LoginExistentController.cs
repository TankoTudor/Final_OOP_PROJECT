using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Final_OOP_PROJECT.Models;

namespace Final_OOP_PROJECT.Controllers
{
    public class LoginExistentController : Controller
    {
        private Individ.IndividDBContext iDb = new Individ.IndividDBContext();

        // GET: LoginExistent
        public ActionResult Index()
        {
            return View(iDb.User.ToList());
        }
    }
}