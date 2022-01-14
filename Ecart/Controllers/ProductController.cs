using Ecart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecart.Controllers
{
    public class ProductController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();
        // GET: Product
        public ActionResult Index()
        {
            using (ecommercedbEntities db = new ecommercedbEntities())
            {

                return View(db.tblproducts.ToList());

            }
                
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.tblcategories, "categoryid", "category");
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblproduct products)
        {
            string fileName = Path.GetFileNameWithoutExtension(products.ImageFile.FileName);
            string extention = Path.GetExtension(products.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
            products.imageurl = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            products.ImageFile.SaveAs(fileName);
            using(ecommercedbEntities db =new ecommercedbEntities())
            {
                db.tblproducts.Add(products);
                db.SaveChanges();

            }
            ModelState.Clear();
            return View();
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}
    }
}