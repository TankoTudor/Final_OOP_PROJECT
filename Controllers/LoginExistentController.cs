﻿using System;
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
        public static int id_forever;
        //private IndividDBContext iDb = new IndividDBContext();

        // GET: LoginExistent
        public ActionResult LoginExistent()
        {
            return View();//iDb.User.ToList()
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LoginExistent(Individ obj)
        {
            if (ModelState.IsValid)
            {
                string Name = Request.Form["Username"].ToString();
                string Password = Request.Form["Password"].ToString();

                using (IndividDBContext iDb = new IndividDBContext())
                {
                    var obj_ = from a in iDb.User
                              where a.Username == Name
                              where a.Password == Password
                              select a;

                    var res = from per in iDb.User
                              select new
                              {
                                  per.IdUser,
                                  per.LastName,
                                  per.FirstName,
                                  per.Username,
                                  per.Password
                              };
                    foreach (var idu in res)
                    {
                        id_forever = idu.IdUser;
                        obj = iDb.User.Find(id_forever);
                    }
                    if (obj_.Any())
                    {
                        if (id_forever == 1)
                            return RedirectToAction("Admin");
                        else
                            return View();
                    }
                }

               
            }
            return View(obj);
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
                using (IndividDBContext rDb = new IndividDBContext())
                {
                    rDb.User.Add(msg);
                    rDb.SaveChanges();

                    return RedirectToAction("LoginExistent");
                }
            }
            return View(msg);
        }

        public ActionResult Admin()
        {
            IndividDBContext iDb = new IndividDBContext();
            return View(iDb.User.ToList());
        }
    }
}