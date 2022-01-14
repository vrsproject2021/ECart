using Ecart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Ecart.Controllers
{
    public class CartController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();
        // GET: Cart
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["UserID"].ToString());
            tblregistration tblregistration= (db.tblregistrations.Where(x => x.userid == userid)).FirstOrDefault();
            //tblcart carts= (db.tblcarts.Where)
            if(tblregistration != null)
            {
                ViewBag.registrationid = tblregistration.registrationid;
            }
            else
            {
                ViewBag.registrationid = 0;
            }
            var cart = db.tblcarts.Include(t => t.tblproduct);
            return View(cart.ToList().Where(a => a.tblproduct.status == "A"));
        }

       
        public ActionResult Addtocart(int? id)
        {
            tblcart cart = new tblcart();
            cart.productid = id;
            cart.userid =Convert.ToInt32(Session["UserID"].ToString());
            db.tblcarts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(string productid,string Quantity,string Total,string price)
        {
            int qty = 0,qtytot=0;
            tblpurchase tblpurchase = new tblpurchase();
            tblpurchase.totalamount = Convert.ToDecimal(Total);
            tblpurchase.userid = Convert.ToInt32(Session["UserID"].ToString());
            tblpurchase.createddate = DateTime.Today.Date;
            db.tblpurchases.Add(tblpurchase);
            db.SaveChanges();
            tblpurchasedetail tblpurchasedetail = new tblpurchasedetail();
            tblpurchasedetail.purchaseid = tblpurchase.purchaseid;
            tblpurchasedetail.productid = Convert.ToInt32(productid);
            tblpurchasedetail.quantity = Convert.ToInt32(Quantity);
            tblpurchasedetail.amount = Convert.ToDecimal(price);
            db.tblpurchasedetails.Add(tblpurchasedetail);
            db.SaveChanges();
            tblstockdetail tblstockdetail = new tblstockdetail();
            tblstockdetail tblstockdetail1 = (db.tblstockdetails.Where(x => x.productid == Convert.ToInt32(productid))).FirstOrDefault();
            if (tblstockdetail != null)
            {
                qty = Convert.ToInt32(((db.tblstockdetails.Where(x => x.productid == Convert.ToInt32(productid))).FirstOrDefault()).availablequantity);

            }
            qtytot = qty - Convert.ToInt32(Quantity);
            tblstockdetail.productid = Convert.ToInt32(productid);
            tblstockdetail.availablequantity = qtytot;
            db.Entry(tblstockdetail).State = EntityState.Modified;

            return View();
        }
    }
}