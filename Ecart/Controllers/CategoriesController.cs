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
    public class CategoriesController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();

        // GET: Categories
        public ActionResult Index(string sortOrder, string searchstring)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var pro = from s in db.tblcategories
                      select s;
            switch (sortOrder)
            {
                case "Name_desc":
                    pro = pro.OrderByDescending(s => s.category);
                    break;
                case "Date":
                    pro = pro.OrderBy(s => s.createddate);
                    break;
                case "Date_desc":
                    pro = pro.OrderByDescending(s => s.createddate);
                    break;
                default:
                    pro = pro.OrderBy(s => s.category);
                    break;
            }
            return View(pro.ToList());
            if(searchstring!=null)
            {

            }
        }
        public ActionResult Search(string searchstring, int? i)
        {
            var departmentResult = from tblcategory in db.tblcategories.Where(m => m.status == "A") select tblcategory;
            var DepatementList = departmentResult.ToList();
            var Mangement = (from c in DepatementList
                             where c.category.Contains(searchstring) ||
                             c.category.ToString().Contains(searchstring) ||
                             c.tbllogin.username.Contains(searchstring) ||

                             c.status.Contains(searchstring)
                             select c).ToList();
            return View("index", Mangement.ToPagedList(i ?? 1, 2));

        }
        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcategory tblcategory = db.tblcategories.Find(id);
            if (tblcategory == null)
            {
                return HttpNotFound();
            }
            return View(tblcategory);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryid,category,userid,createddate")] tblcategory tblcategory)
        {
            if (ModelState.IsValid)
            {
                tblcategory.userid = Convert.ToInt32(Session["UserID"].ToString());
                tblcategory.createddate = DateTime.Today.Date;
                db.tblcategories.Add(tblcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblcategory.userid);
            return View(tblcategory);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcategory tblcategory = db.tblcategories.Find(id);
            if (tblcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblcategory.userid);
            return View(tblcategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryid,category,userid,createddate")] tblcategory tblcategory)
        {
            if (ModelState.IsValid)
            {
                tblcategory.userid = Convert.ToInt32(Session["UserID"].ToString());
                tblcategory.createddate = DateTime.Today.Date;
                db.Entry(tblcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblcategory.userid);
            return View(tblcategory);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcategory tblcategory = db.tblcategories.Find(id);
            if (tblcategory == null)
            {
                return HttpNotFound();
            }
            return View(tblcategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblcategory tblcategory = db.tblcategories.Find(id);
            db.tblcategories.Remove(tblcategory);
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
