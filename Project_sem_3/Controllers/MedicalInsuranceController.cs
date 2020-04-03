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
            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 4).Where(i => i.Status == 1);
            ViewBag.insurancepackages = insurancePackages;
            return View();

        }

        public JsonResult ViewProgramme()
        {
            db.Configuration.ProxyCreationEnabled = false;
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
       
        [HttpPost]
        public ActionResult CreateContract(ContractMedical medical)
        {
            var contract = new Contract
            {
                TotalPrice = medical.Totalprice,
                ApplicationUserId = User.Identity.GetUserId(),
                InsuranceId = 4,
                MedicalInsurances = new List<MedicalInsurance>()
            };

            foreach (var item in medical.items)
            {
                var medicalinsurace = new MedicalInsurance()
                {
                    InsurancePackageId = System.Int32.Parse(item.InsurancePackageId),
                    ProgrammeId = System.Int32.Parse(item.Programmeid),
                    Name = item.Name,
                    ContractId = contract.Id,
                    PhoneNumber = item.PhoneNumber,
                    Email = item.Email,
                    Address = item.Address,
                    Quantity = 1,
                    UnitPrice = item.unitprice,
                    DateOfBirth = item.DateOfbirth
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
            return Redirect("/OrderNotice/Index");


        }
    }
}