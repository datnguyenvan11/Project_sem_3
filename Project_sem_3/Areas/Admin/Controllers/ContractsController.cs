﻿using System;
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
    public class ContractsController : Controller
    {
        private MyDb db = new MyDb();

        // GET: Admin/Contracts
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.Insurance);
            return View(contracts.ToList());
        }

        // GET: Admin/Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Admin/Contracts/Create
        public ActionResult Create()
        {
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name");
            return View();
        }

        // POST: Admin/Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,TotalPrice,InsuranceId,CreatedAt,UpdatedAt,DeletedAt,Status")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", contract.InsuranceId);
            return View(contract);
        }

        // GET: Admin/Contracts/Edit/5
     
        // POST: Admin/Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

      

        // GET: Admin/Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Admin/Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StatusOrder(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Contract contract = db.Contracts.Find(IDs);
                db.Contracts.Attach(contract);
                contract.Status = action;
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
