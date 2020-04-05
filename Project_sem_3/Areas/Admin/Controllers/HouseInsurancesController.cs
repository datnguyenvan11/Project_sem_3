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
    [Authorize(Roles = "Admin")]

    public class HouseInsurancesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/HouseInsurances
        public ActionResult Index(int Id)
        {
            var houseInsurances = db.HouseInsurances.Include(h => h.Contract).Include(h => h.InsurancePackage).Where(x=>x.ContractId==Id);
            return View(houseInsurances.ToList());
        }

        // GET: Admin/HouseInsurances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseInsurance houseInsurance = db.HouseInsurances.Find(id);
            if (houseInsurance == null)
            {
                return HttpNotFound();
            }
            return View(houseInsurance);
        }

        // GET: Admin/HouseInsurances/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Id");
            ViewBag.InsurancePackageId = new SelectList(db.InsurancePackages, "Id", "Name");
            return View();
        }

        // POST: Admin/HouseInsurances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsurancePackageId,ContractId,HouseType,DurationHouse,HouseOwner,HouserAddress,Quantity,UnitPrice")] HouseInsurance houseInsurance)
        {
            if (ModelState.IsValid)
            {
                db.HouseInsurances.Add(houseInsurance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Id", houseInsurance.ContractId);
            ViewBag.InsurancePackageId = new SelectList(db.InsurancePackages, "Id", "Name", houseInsurance.InsurancePackageId);
            return View(houseInsurance);
        }

        // GET: Admin/HouseInsurances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseInsurance houseInsurance = db.HouseInsurances.Find(id);
            if (houseInsurance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Id", houseInsurance.ContractId);
            ViewBag.InsurancePackageId = new SelectList(db.InsurancePackages, "Id", "Name", houseInsurance.InsurancePackageId);
            return View(houseInsurance);
        }

        // POST: Admin/HouseInsurances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsurancePackageId,ContractId,HouseType,DurationHouse,HouseOwner,HouserAddress,Quantity,UnitPrice")] HouseInsurance houseInsurance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseInsurance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.Contracts, "Id", "Id", houseInsurance.ContractId);
            ViewBag.InsurancePackageId = new SelectList(db.InsurancePackages, "Id", "Name", houseInsurance.InsurancePackageId);
            return View(houseInsurance);
        }

        // GET: Admin/HouseInsurances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseInsurance houseInsurance = db.HouseInsurances.Find(id);
            if (houseInsurance == null)
            {
                return HttpNotFound();
            }
            return View(houseInsurance);
        }

        // POST: Admin/HouseInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseInsurance houseInsurance = db.HouseInsurances.Find(id);
            db.HouseInsurances.Remove(houseInsurance);
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
