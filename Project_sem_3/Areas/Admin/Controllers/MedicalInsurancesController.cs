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
    [Authorize(Roles = "Admin")]

    public class MedicalInsurancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/MedicalInsurances
        public ActionResult Index(int Id)
        {
            var medicalInsurances = db.MedicalInsurances.Include(m => m.Contract).Include(m => m.InsurancePackage).Include(m => m.Programme).Where(m=>m.ContractId==Id);
            return View(medicalInsurances.ToList());
        }

        // GET: Admin/MedicalInsurances/Details/5
       
    }
}
