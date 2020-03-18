using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class HomeInsuranceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: HomeInsurance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {

            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 19);
            return View(insurancePackages);
        }

        public ActionResult CreateContract(int InsurancePackageId, string HouseType, string DurationHouse, string HouseOwner, string HouserAddress)
        {
            // load cart trong session.
            var insurancepackage = db.InsurancePackages.Find(InsurancePackageId);

            var contract = new Contract
            {
                TotalPrice = insurancepackage.Price,
                ApplicationUserId = "1",
                InsuranceId = 19,
                HouseInsurances = new List<HouseInsurance>()
            };


            var houseinsurance = new HouseInsurance()
            {
                InsurancePackageId = insurancepackage.Id,
                HouseOwner = HouseOwner,
                HouseType = HouseType,
                HouserAddress = HouserAddress,
                DurationHouse = DurationHouse,
                ContractId = contract.Id,
                Quantity = 1,
                UnitPrice = insurancepackage.Price
            };
            contract.HouseInsurances.Add(houseinsurance);
            db.Contracts.Add(contract);


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