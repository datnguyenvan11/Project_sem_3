using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{

    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult HouseInsurance()
        {
            return View();
        }
        public ActionResult MotorInsurance()
        {
            var insurances = db.Insurances.Where(x => x.Status == 0).FirstOrDefault(x=>x.Id==27);
            return View(insurances);
        }
        public ActionResult MedicalInsurance()
        {
            return View();
        }
        public ActionResult lifeInsurance()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}