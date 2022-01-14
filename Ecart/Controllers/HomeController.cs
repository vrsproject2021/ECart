using Ecart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ecart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(ecommercedbEntities db= new ecommercedbEntities())
            {
                ViewBag.categoryid = new SelectList(db.tblcategories, "categoryid", "category").ToList();
                return View(db.tblproducts.ToList().Where(a=> a.status=="A"));
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Cart()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Details(int? id)
        //{
        //    ECartDBEntities1 db = new ECartDBEntities1();

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CartItem cartItem = db.CartItems.Find(id);
        //    if (cartItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cartItem);
        //}
    }
}