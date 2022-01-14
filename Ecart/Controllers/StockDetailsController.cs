using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecart.Models;
using PagedList;

namespace Ecart.Controllers
{
    public class StockDetailsController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();

        // GET: StockDetails
        public ActionResult Index(string searchstring)
        {
            var tblstockdetails = db.tblstockdetails.Include(t => t.tblproduct);
            return View(tblstockdetails.ToList());
        }

        //public ActionResult Search(string searchstring, int? i)
        //{
        //    var departmentResult = from tblstockdetail in db.tblstockdetails.Where(m => m.status == "A") select tblproduct;
        //    var DepatementList = departmentResult.ToList();
        //    var Mangement = (from c in DepatementList
        //                     where c.productname.Contains(searchstring) ||
        //                     c.Quantity.ToString().Contains(searchstring) ||
        //                     c.tblcategory.category.Contains(searchstring) ||

        //                     c.status.Contains(searchstring)
        //                     select c).ToList();
        //    return View("index", Mangement.ToPagedList(i ?? 1, 2));

        //}
        // GET: StockDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblstockdetail tblstockdetail = db.tblstockdetails.Find(id);
            if (tblstockdetail == null)
            {
                return HttpNotFound();
            }
            return View(tblstockdetail);
        }

        // GET: StockDetails/Create
        public ActionResult Create()
        {
            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname");
            return View();
        }

        // POST: StockDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stockid,productid,availablequantity")] tblstockdetail tblstockdetail)
        {
            if (ModelState.IsValid)
            {
                db.tblstockdetails.Add(tblstockdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblstockdetail.productid);
            return View(tblstockdetail);
        }

        // GET: StockDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblstockdetail tblstockdetail = db.tblstockdetails.Find(id);
            if (tblstockdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblstockdetail.productid);
            return View(tblstockdetail);
        }

        // POST: StockDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stockid,productid,availablequantity")] tblstockdetail tblstockdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblstockdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productid = new SelectList(db.tblproducts, "productid", "productname", tblstockdetail.productid);
            return View(tblstockdetail);
        }

        // GET: StockDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblstockdetail tblstockdetail = db.tblstockdetails.Find(id);
            if (tblstockdetail == null)
            {
                return HttpNotFound();
            }
            return View(tblstockdetail);
        }

        // POST: StockDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblstockdetail tblstockdetail = db.tblstockdetails.Find(id);
            db.tblstockdetails.Remove(tblstockdetail);
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
