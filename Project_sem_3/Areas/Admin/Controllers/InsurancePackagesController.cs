using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class InsurancePackagesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/InsurancePackages
        public ActionResult Index()
        {
            var insurancePackages = db.InsurancePackages.Include(i => i.Insurance).Where(x =>x.Status == 0);
            return View(insurancePackages.ToList());
        }

        // GET: Admin/InsurancePackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Create
        public ActionResult Create()
        {
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name");
            return View();
        }

        // POST: Admin/InsurancePackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InsuranceId,Name,Description,Price,DurationContract,DurationPay,CreatedAt,UpdatedAt,DeleteAt")] InsurancePackage insurancePackage)
        {
            if (ModelState.IsValid)
            {
                var insurancepackages = new InsurancePackage()
                {
                    InsuranceId=insurancePackage.InsuranceId,
                    Name=insurancePackage.Name,
                    Description=insurancePackage.Description,
                    Price=insurancePackage.Price,
                    DurationContract=insurancePackage.DurationContract,
                    DurationPay=insurancePackage.DurationPay,
                    CreatedAt=DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    DeleteAt=DateTime.Now,
                };
                db.InsurancePackages.Add(insurancepackages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // POST: Admin/InsurancePackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InsuranceId,Name,Description,Price,DurationContract,DurationPay,CreatedAt,UpdatedAt,DeleteAt")] InsurancePackage insurancePackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insurancePackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            return View(insurancePackage);
        }

        // POST: Admin/InsurancePackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            db.InsurancePackages.Remove(insurancePackage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll(int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                InsurancePackage insurancePackage = db.InsurancePackages.Find(IDs);
                db.InsurancePackages.Attach(insurancePackage);
                insurancePackage.Status = -1;
            }
            db.SaveChanges();
            return Json(selectedIDs, JsonRequestBehavior.AllowGet);
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
