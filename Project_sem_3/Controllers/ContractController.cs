using Microsoft.AspNet.Identity;
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
        [Authorize]
        public ActionResult Index()
        {
            string userIds = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == userIds);
            ViewBag.currentuser = currentUser;
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