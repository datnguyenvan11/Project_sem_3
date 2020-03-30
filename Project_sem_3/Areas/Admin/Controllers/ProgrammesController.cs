using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class ProgrammesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        List<Programme> list = new List<Programme>();
        // GET: Admin/Programmes
        public ActionResult Index(string sortOrder, string searchString, int? page, int? pageSize)
        {
            var programme = db.Programmes.Where(t => t.Status == 1);
            if (!String.IsNullOrEmpty(searchString))
            {
                programme = db.Programmes.Where(t => t.Name.Contains(searchString))
                    .Where(t => t.Status == 0);
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
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            switch (sortOrder)
            {
                case "Name_desc":
                    programme = programme.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    programme = programme.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    programme = programme.OrderByDescending(s => s.Price);
                    break;
                default:
                    programme = programme.OrderBy(s => s.Name);
                    break;
            }
            return View(programme.ToList().ToPagedList(pageNumber, pagesize));

        }

        // GET: Admin/Programmes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            return View(programme);
        }

        // GET: Admin/Programmes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Programmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                list.Add(programme);
                FileStream fs = new FileStream("C:/Users/Windows 10 Version 2/Desktop/test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(fs, Encoding.UTF8);
                streamWriter.WriteLine(new JavaScriptSerializer().Serialize(list));
                streamWriter.Flush();
                fs.Close();
                db.Programmes.Add(programme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(programme);
        }

        // GET: Admin/Programmes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            return View(programme);
        }

        // POST: Admin/Programmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Programme programme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        // GET: Admin/Programmes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programme programme = db.Programmes.Find(id);
            if (programme == null)
            {
                return HttpNotFound();
            }
            return View(programme);
        }

        // POST: Admin/Programmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Programme programme = db.Programmes.Find(id);
            db.Programmes.Remove(programme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Programme programme = db.Programmes.Find(IDs);
                db.Programmes.Attach(programme);
                programme.Status = action;
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
                Programme programme = db.Programmes.Find(IDs);
                db.Programmes.Attach(programme);
                programme.Status = action;
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

            var programme = db.Programmes.Where(t => t.Status == -1);
            if (!String.IsNullOrEmpty(searchString))
            {
                programme = db.Programmes.Where(t => t.Name.Contains(searchString))
                    .Where(t => t.Status == -1);
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
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            switch (sortOrder)
            {
                case "Name_desc":
                    programme = programme.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    programme = programme.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    programme = programme.OrderByDescending(s => s.Price);
                    break;
                default:
                    programme = programme.OrderBy(s => s.Name);
                    break;
            }
            return View(programme.ToList().ToPagedList(pageNumber, pagesize));
        }
    }
}
