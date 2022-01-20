using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Final_OOP_PROJECT.Models;

namespace Final_OOP_PROJECT.Controllers
{
    public class GamesController : Controller
    {
        private GamersDbContext gDb = new GamersDbContext();
        // GET: Games
        public ActionResult Catalog()
        {
            return View(gDb.Produse.ToList());
        }

        public ActionResult CatalogAdmin()
        {
            return View(gDb.Produse.ToList());
        }

        public ActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGame(Gamers gm)
        {
            if (ModelState.IsValid)
            {
                using (GamersDbContext gDb = new GamersDbContext())
                {
                    gDb.Produse.Add(gm);
                    gDb.SaveChanges();

                    return RedirectToAction("CatalogAdmin");
                }
            }
            return View(gm);
        }
    }
}