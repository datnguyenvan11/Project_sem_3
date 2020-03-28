﻿using Microsoft.AspNet.Identity;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class LifeInsuranceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LifeInsurance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 26);
            return View(insurancePackages);
        }
        [HttpPost]
        public ActionResult CreateContract(int InsurancePackageId, string name, string email, string address, DateTime Date_Iden, string Place_Iden, string phone, string Job, string IdentityCard)
        {
            var insurancepackage = db.InsurancePackages.Find(InsurancePackageId);

            var contract = new Contract
            {
                TotalPrice = insurancepackage.Price,
                ApplicationUserId = "1",
                InsuranceId = 26,
                LifeInsurances = new List<LifeInsurance>()
            };


            var lifeInsurance = new LifeInsurance()
            {
                InsurancePackageId = insurancepackage.Id,
                ContractId = contract.Id,
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Address = address,
                IdentityCard = IdentityCard,
                DateOfIdentityCard = Date_Iden,
                PlaceOfIdentityCard = Place_Iden,
                Job = Job,
                Quantity = 1,
                UnitPrice = insurancepackage.Price
            };
            contract.LifeInsurances.Add(lifeInsurance);
            db.Contracts.Add(contract);


            db.SaveChanges();

            var transaction = db.Database.BeginTransaction();
            try
            {
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            return Redirect("/MotorCart/ShowCart");


        }
    }
}