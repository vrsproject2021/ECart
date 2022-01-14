using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecart.Models;

namespace Ecart.Controllers
{
    public class PurchaseDetailsController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();

        // GET: PurchaseDetails
        public ActionResult Index()
        {
            var tblpurchasedetails = db.tblpurchasedetails.Include(t => t.tblproduct).Include(t => t.tblpurchase);
            return View(tblpurchasedetails.ToList());
        }

        // GET: PurchaseDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpurchasedetail tblpurchasedetail = db.tblpurchasedetails.Find(id);
            if (tblpurchasedetail == null)
            {
                return HttpNotFound();
            }
            return View(tblpurchasedetail);
        }

        // GET: PurchaseDetails/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.tblcategories, "categoryid", "category");
            //ViewBag.purchaseid = new SelectList(db.tblpurchases, "purchaseid", "invoicenumber");
            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchasedetailsid,purchaseid,productid,quantity,amount,tblproduct")] tblpurchasedetail tblpurchasedetail)
        {
            string fileName = Path.GetFileNameWithoutExtension(tblpurchasedetail.tblproduct.ImageFile.FileName);
            string extention = Path.GetExtension(tblpurchasedetail.tblproduct.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
            tblpurchasedetail.tblproduct.imageurl = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            tblpurchasedetail.tblproduct.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.tblpurchasedetails.Add(tblpurchasedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblpurchasedetail.productid);
            ViewBag.purchaseid = new SelectList(db.tblpurchases, "purchaseid", "invoicenumber", tblpurchasedetail.purchaseid);
            return View(tblpurchasedetail);
        }

        // GET: PurchaseDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpurchasedetail tblpurchasedetail = db.tblpurchasedetails.Find(id);
            if (tblpurchasedetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblpurchasedetail.productid);
            ViewBag.purchaseid = new SelectList(db.tblpurchases, "purchaseid", "invoicenumber", tblpurchasedetail.purchaseid);
            return View(tblpurchasedetail);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchasedetailsid,purchaseid,productid,quantity,amount")] tblpurchasedetail tblpurchasedetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblpurchasedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblpurchasedetail.productid);
            ViewBag.purchaseid = new SelectList(db.tblpurchases, "purchaseid", "invoicenumber", tblpurchasedetail.purchaseid);
            return View(tblpurchasedetail);
        }

        // GET: PurchaseDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpurchasedetail tblpurchasedetail = db.tblpurchasedetails.Find(id);
            if (tblpurchasedetail == null)
            {
                return HttpNotFound();
            }
            return View(tblpurchasedetail);
        }

        // POST: PurchaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblpurchasedetail tblpurchasedetail = db.tblpurchasedetails.Find(id);
            db.tblpurchasedetails.Remove(tblpurchasedetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
