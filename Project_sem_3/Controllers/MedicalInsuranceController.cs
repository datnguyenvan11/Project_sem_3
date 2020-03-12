using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class MedicalInsuranceController : Controller
    {
        // GET: MedicalInsurance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Information()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
    }
}