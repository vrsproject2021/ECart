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
using PagedList.Mvc;
using PagedList;

namespace Ecart.Controllers
{
    public class ProductsManController : Controller
    {
        private ecommercedbEntities db = new ecommercedbEntities();
        
        // GET: ProductsMan
        public ActionResult Index(string searchstring,int?i)
        {

            var tblproducts = db.tblproducts.Include(t => t.tbllogin).Include(t => t.tblcategory);
            var departmentResult = from tblproduct in db.tblproducts.Where(m => m.status == "A") select tblproduct;
            var DepatementList = departmentResult.ToList();
            //return View(tblproducts.ToList());
            if (searchstring !=null)
            {
               
                var Mangement = (from c in DepatementList
                                 where c.productname.Contains(searchstring) ||
                                 c.Quantity.ToString().Contains(searchstring) ||
                                 c.tblcategory.category.Contains(searchstring) ||

                                 c.status.Contains(searchstring)
                                 select c).ToList();
                int Size_Of_Page = 4;
                int No_Of_Page = (i ?? 1);
                return View(Mangement.ToPagedList(No_Of_Page, Size_Of_Page));

            }
            else
            {
                var Mangement = (from c in DepatementList select c).ToList();
                int Size_Of_Page = 4;
                int No_Of_Page = (i ?? 1);
                return View(Mangement.ToPagedList(No_Of_Page, Size_Of_Page));
            }

        }
        public ActionResult Search(string searchstring, int? i)
        {
            var departmentResult = from tblproduct in db.tblproducts.Where(m => m.status == "A") select tblproduct;
            var DepatementList = departmentResult.ToList();
            var Mangement = (from c in DepatementList
                             where c.productname.Contains(searchstring) ||
                             c.Quantity.ToString().Contains(searchstring) ||
                             c.tblcategory.category.Contains(searchstring) ||
                            
                             c.status.Contains(searchstring)
                             select c).ToList();
            int Size_Of_Page = 4;
            int No_Of_Page = (i ?? 1);
            return View("Index", Mangement.ToPagedList(No_Of_Page, Size_Of_Page));

        }


        // GET: ProductsMan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // GET: ProductsMan/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.tblcategories, "categoryid", "category");
            return View();
        }

        // POST: ProductsMan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,productname,categoryid,price,imageurl,userid,createddate,ImageFile,Quantity")] tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(tblproduct.ImageFile.FileName);
                string extention = Path.GetExtension(tblproduct.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
                tblproduct.imageurl = "../Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
                tblproduct.ImageFile.SaveAs(fileName);
                tblproduct.userid = Convert.ToInt32(Session["UserID"].ToString());
                tblproduct.createddate = DateTime.Today.Date;
                db.tblproducts.Add(tblproduct);
                db.SaveChanges();

                tblstockdetail tblstockdetail = new tblstockdetail();
                tblstockdetail.productid = tblproduct.productid;
                tblstockdetail.availablequantity = tblproduct.Quantity;
                db.tblstockdetails.Add(tblstockdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblproduct.userid);
            return View(tblproduct);
        }

        // GET: ProductsMan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            tblstockdetail tblstockdetail = (db.tblstockdetails.Where(x => x.productid == tblproduct.productid)).FirstOrDefault();
            if(tblstockdetail !=null)
            {
                tblproduct.Quantity = Convert.ToInt32(((db.tblstockdetails.Where(x => x.productid == tblproduct.productid)).FirstOrDefault()).availablequantity);

            }
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.tblcategories, "categoryid", "category");
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblproduct.userid);
            return View(tblproduct);
        }

        // POST: ProductsMan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,productname,categoryid,price,imageurl,userid,createddate,ImageFile")] tblproduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(tblproduct.ImageFile.FileName);
                string extention = Path.GetExtension(tblproduct.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
                tblproduct.imageurl = "../Image/" + fileName;
             fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
                tblproduct.ImageFile.SaveAs(fileName);
                tblproduct.userid = Convert.ToInt32(Session["UserID"].ToString());
                tblproduct.createddate = DateTime.Today.Date;
                db.Entry(tblproduct).State = EntityState.Modified;
                db.SaveChanges();
                tblstockdetail tblstockdetail = new tblstockdetail();
                tblstockdetail.productid = tblproduct.productid;
                tblstockdetail.availablequantity = tblproduct.Quantity;
                db.Entry(tblstockdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.tbllogins, "userid", "username", tblproduct.userid);
            return View(tblproduct);
        }

        // GET: ProductsMan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblproduct tblproduct = db.tblproducts.Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        // POST: ProductsMan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblproduct tblproduct = db.tblproducts.Find(id);
            ////db.tblproducts.Remove(tblproduct);
            tblproduct.status ="D";
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
