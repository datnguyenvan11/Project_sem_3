using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class ContractController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Contract
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContract(Contract contract)
        {
            db.Contracts.Add(contract);
            db.SaveChanges();
            return Json(contract, JsonRequestBehavior.AllowGet);
        }
    }
}