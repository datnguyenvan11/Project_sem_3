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
    public class ContractsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Contracts
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.Insurance).Where(x => x.Status == 1);
            return View(contracts.ToList());
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Deleted()
        {

            return View(db.Contracts.Where(t => t.Status == -1).ToList());
        }
    }
}
