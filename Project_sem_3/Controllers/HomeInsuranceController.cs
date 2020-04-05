using Microsoft.AspNet.Identity;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 1);
            return View(insurancePackages);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult CreateContract(int InsurancePackageId, string HouseType, string DurationHouse, string HouseOwner, string HouserAddress)
        {
            // load cart trong session.
            var insurancepackage = db.InsurancePackages.Find(InsurancePackageId);

            var contract = new Contract
            {
                TotalPrice = insurancepackage.Price,

                ApplicationUserId = User.Identity.GetUserId(),
                InsuranceId = 1,
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
                var em = db.Users.Where(x => x.Id == contract.ApplicationUserId).FirstOrDefault();
                var senderemail = new MailAddress("nguyenvandatvtacl16@gmail.com", "Insurance Company");
                var receivermail = new MailAddress(em.Email, "Insurance Company");
                var passwordemail = "vtacl123";
                var subject = "Notification Order";
                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">" +
                    "<style>table, td, th {border: 1px solid black; font-size: 15px}</style>" +
                    "<style> p {font-size: 18px}</style>"
                    ;
                body += "<p>" + "Your order information is : " + "</p>"
                 + "<p>" + "Name Insurance : HouseInsurance</p>"
                 + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                "<br>";
                body += "</HEAD><BODY>" +
                    "<tr>" +
                    "<th>Package Insurance</th>" +
                    "<th>HouseOwner</th>" +
                    "<th>HouseType</th>" +
                    "<th>HouserAddress</th>" +
                    "<th>DurationHouse</th>" +
                    "<th>Quantity</th>" +
                    "<th>UnitPrice</th>" +
                    "</tr>";
                body +=
                    "<tr>" +
                    "<td>" + insurancepackage.Name + "</td>" +
                    "<td>" + HouseOwner + "</td>" +
                    "<td>" + HouseType + "</td>" +
                    "<td>" + HouserAddress + "</td>" +
                    "<td>" + DurationHouse + "</td>" +
                    "<td>" + 1 + "</td>" +
                    "<td>" + insurancepackage.Price + "</td>" +
                    "</tr>";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderemail.Address, passwordemail)
                };
                using (var mess = new MailMessage(senderemail, receivermail)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                }
                )
                {
                    smtp.Send(mess);
                };
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