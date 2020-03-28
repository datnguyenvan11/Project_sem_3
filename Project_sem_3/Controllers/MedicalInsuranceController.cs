using Microsoft.AspNet.Identity;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 17).Where(i => i.Status == -1);
            ViewBag.insurancepackages = insurancePackages;
            return View();

        }

        public JsonResult ViewProgramme()
        {
            var programme = db.Programmes.Where(x => x.Status == 0).ToList();

            return Json(new { programme }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Order(int number, double price)
        {
            TempData["number"] = number;
            TempData["price"] = price;
            return RedirectToAction("Order");
        }
        public ActionResult getsesstion()
        {
            return View();
        }
        public ActionResult CreateContract(ContractMedical medical)
        {
            // load cart trong session.
            var insurancepackage = db.InsurancePackages.Find(medical.InsurancePackageId);
            var contract = new Contract
            {
                TotalPrice = Convert.ToInt32(medical.Totalprice),
                UserId = User.Identity.GetUserId(),
                InsuranceId = 19,
                MedicalInsurances = new List<MedicalInsurance>()
            };

            foreach (var item in medical.Items)
            {
                DateTime dt = DateTime.ParseExact(item.DateOfbirth, "yyyy mm dd ", CultureInfo.InvariantCulture);
                var medicalinsurace = new MedicalInsurance()
                {
                    InsurancePackageId = insurancepackage.Id,
                    ProgrammeId =Convert.ToInt32(item.Programmeid) ,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Address    = item.Address,          
                    Quantity = 1,
                    UnitPrice = Convert.ToInt32(item.unitprice),
                    DateOfBirth = dt
                };
                contract.MedicalInsurances.Add(medicalinsurace);
                db.Contracts.Add(contract);
            }

            db.SaveChanges();

            // lưu vào database.
            var transaction = db.Database.BeginTransaction();
            try
            {

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
            }
            return Redirect("/MotorCart/ShowCart");


        }
    }
}