using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class ContractsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Contracts
        public ActionResult Index(string sortOrder, string searchString, int? page, int? pageSize)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 1);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    contracts = db.Contracts.Where(x => x.TotalPrice.Contains(searchString))
            //        .Where(t => t.Status == 1);
            //}
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }
            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }

        // GET: Admin/Contracts/Details/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Contract contract = db.Contracts.Find(IDs);
                db.Contracts.Attach(contract);
                contract.Status = 0;
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

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == -1);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    contracts = db.Contracts.Where(x => x.TotalPrice.Contains(searchString))
            //        .Where(t => t.Status == -1);
            //}
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }
            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }

        public ActionResult Confirmed(string sortOrder, string searchString, int? page, int? pageSize)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 0);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    contracts = db.Contracts.Where(x => x.TotalPrice.Contains(searchString))
            //        .Where(t => t.Status == 0);
            //}
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }
            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }
    }
}
