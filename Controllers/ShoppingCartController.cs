using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Final_OOP_PROJECT.Models;
namespace Final_OOP_PROJECT.Controllers
{
    public class ShoppingCartController : Controller
    {
        private GamersDbContext gdb = new GamersDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult AddToCart(int gid)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<StatusCart> cart = new List<StatusCart>();
        //        var product = ctx.Tbl_Product.Find(productId);
        //        cart.Add(new StatusCart()
        //        {
        //            Product = product,
                  
        //        });
        //        Session["cart"] = cart;
        //    }

        //    else
        //    {
        //        List<StatusCart> cart = (List<StatusCart>)Session["cart"];
        //        var product = ctx.Tbl_Product.Find(productId);
        //        var product = 
        //        cart.Add(new StatusCart()
        //        {
        //            Product = product,
                
        //        });
        //        Session["cart"] = cart;
        //    }
        //    return Redirect("Index");
        //}
    }
}