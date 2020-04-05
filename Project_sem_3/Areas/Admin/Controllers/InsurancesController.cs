using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class InsurancesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Insurances
        public ActionResult Index(string sortOrder, string searchString, int? page, int? pageSize, string listInsurance)
        {
            var insurances = db.Insurances.Where(x => x.Status == 1);
            if (!String.IsNullOrEmpty(searchString))
            {
                insurances = db.Insurances.Where(x => x.Name.Contains(searchString))
                    .Where(x => x.Status == 0);
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            ViewBag.listInsurance = new List<SelectListItem>()
            {
                new SelectListItem() { Text="List", Value= "0" },
                new SelectListItem() { Text="Delete", Value= "-1"},
            };
            switch (listInsurance)
            {
                case "0":
                    insurances = db.Insurances.Where(x => x.Status == 1);
                    break;
                case "-1":
                    insurances = db.Insurances.Where(x => x.Status == -1);
                    break;
            }
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.psize = pagesize;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Name_desc":
                    insurances = insurances.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    insurances = insurances.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    insurances = insurances.OrderByDescending(s => s.CreatedAt);
                    break;

                default:
                    insurances = insurances.OrderBy(s => s.Name);
                    break;
            }
            return View(insurances.ToList().ToPagedList(pageNumber, pagesize));
        }

        // GET: Admin/Insurances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insurance insurance = db.Insurances.Find(id);
            if (insurance == null)
            {
                return HttpNotFound();
            }
            return View(insurance);
        }

        // GET: Admin/Insurances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Insurances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                var insurances = new Insurance()
                {
                    Name = insurance.Name,
                    Description = insurance.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeleteAt = DateTime.Now
                };
                db.Insurances.Add(insurances);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insurance);
        }

        // GET: Admin/Insurances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insurance insurance = db.Insurances.Find(id);
            if (insurance == null)
            {
                return HttpNotFound();
            }
            return View(insurance);
        }

        // POST: Admin/Insurances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeleteAt")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insurance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insurance);
        }

        // GET: Admin/Insurances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insurance insurance = db.Insurances.Find(id);
            if (insurance == null)
            {
                return HttpNotFound();
            }
            return View(insurance);
        }

        // POST: Admin/Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insurance insurance = db.Insurances.Find(id);
            db.Insurances.Remove(insurance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Insurance insurance = db.Insurances.Find(IDs);
                db.Insurances.Attach(insurance);
                insurance.Status = action;
            }
            db.SaveChanges();
            return Json(selectedIDs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Insurance insurance = db.Insurances.Find(IDs);
                db.Insurances.Attach(insurance);
                insurance.Status = action;
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

        public ActionResult Deleted(string sortOrder, string searchString, int? page, int? pageSize)
        {

            var insurances = db.Insurances.Where(x => x.Status == -1);
            if (!String.IsNullOrEmpty(searchString))
            {
                insurances = db.Insurances.Where(x => x.Name.Contains(searchString))
                    .Where(x => x.Status == -1);
            }

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Name_desc":
                    insurances = insurances.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    insurances = insurances.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    insurances = insurances.OrderByDescending(s => s.CreatedAt);
                    break;

                default:
                    insurances = insurances.OrderBy(s => s.Name);
                    break;
            }
            return View(insurances.ToList().ToPagedList(pageNumber, pagesize));
        }
    }
}
