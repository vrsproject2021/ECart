using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecart.Models;

namespace Ecart.Controllers
{
    public class RegistrationsController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();

        // GET: Registrations
        public ActionResult Index()
        {
            var tblregistrations = db.tblregistrations.Include(t => t.tbllogin);
            return View(tblregistrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblregistration tblregistration = db.tblregistrations.Find(id);
            if (tblregistration == null)
            {
                return HttpNotFound();
            }
            return View(tblregistration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "registrationid,userid,name,addressline1,addresslin2,city,country,phone,pin,email,isactive,createddate")] tblregistration tblregistration)
        {
            if (ModelState.IsValid)
            {
                tblregistration.userid = Convert.ToInt32(Session["UserID"].ToString());
                tblregistration.createddate = DateTime.Today.Date;
                tblregistration.isactive = true;
                db.tblregistrations.Add(tblregistration);
                db.SaveChanges();
                return RedirectToAction("Index","Cart");
            }

            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblregistration.userid);
            return View("Index", "Cart");
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblregistration tblregistration = db.tblregistrations.Find(id);
            if (tblregistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblregistration.userid);
            return View(tblregistration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "registrationid,userid,name,addressline1,addresslin2,city,country,phone,pin,email,isactive,createddate")] tblregistration tblregistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblregistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblregistration.userid);
            return View(tblregistration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblregistration tblregistration = db.tblregistrations.Find(id);
            if (tblregistration == null)
            {
                return HttpNotFound();
            }
            return View(tblregistration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblregistration tblregistration = db.tblregistrations.Find(id);
            db.tblregistrations.Remove(tblregistration);
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
