using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class MedicalInsuranceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: MedicalInsurance
        public ActionResult Information()
        {
            return View(db.InsurancePackages.ToList());
        }
        public ActionResult Order()
        {
            ViewBag.Number = TempData["number"];
            ViewBag.Price = TempData["price"];
            return View(db.Programmes.Where(x => x.Status == 0));
        }

        [HttpPost]
        public ActionResult Order(int number, double price)
        {
            TempData["number"] = number;
            TempData["price"] = price;
            return RedirectToAction("Order");
        }
    }
}