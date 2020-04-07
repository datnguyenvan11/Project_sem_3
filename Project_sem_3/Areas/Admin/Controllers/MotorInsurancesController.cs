using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MotorInsurancesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/MotorInsurances
        public ActionResult Index(int Id)
        {
            var motorInsurances = db.MotorInsurances.Include(m => m.Contract).Include(m => m.InsurancePackage).Where(x=>x.ContractId==Id);
            return View(motorInsurances.ToList());
        }

    }
}
