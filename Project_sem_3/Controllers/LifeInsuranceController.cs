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
            var insurancePackages = db.InsurancePackages.Where(i => i.InsuranceId == 2).Where(i => i.Status == 1);
            ViewBag.insurancepackages = insurancePackages;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]

        public ActionResult CreateContract(int InsurancePackageId, string name, string email, string address, DateTime Date_Iden, string Place_Iden, string phone, string Job, string IdentityCard, string MaritalStatus)
        {
            var insurancepackage = db.InsurancePackages.Find(InsurancePackageId);

            var contract = new Contract
            {
                TotalPrice = insurancepackage.Price,
                ApplicationUserId = User.Identity.GetUserId(),
                InsuranceId = 2,
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
                MaritalStatus= MaritalStatus,
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
                 + "<p>" + "Name Insurance : LifeInsurance</p>"
                 + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                "<br>";
                body += "</HEAD><BODY>" +
                    "<tr>" +
                    "<th>Package Insurance</th>" +
                    "<th>Name</th>" +
                    "<th>PhoneNumber</th>" +
                    "<th>Email</th>" +
                    "<th>Address</th>" +
                    "<th>IdentityCard</th>" +
                    "<th>DateOfIdentityCard</th>" +
                    "<th>PlaceOfIdentityCard</th>" +
                    "<th>Job</th>" +
                    "<th>Quantity</th>" +
                    "<th>UnitPrice</th>" +
                    "</tr>";
                    body +=
                        "<tr>" +
                        "<td>" + insurancepackage.Name + "</td>" +
                        "<td>" + name + "</td>" +
                        "<td>" + phone + "</td>" +
                        "<td>" + email + "</td>" +
                        "<td>" + address + "</td>" +
                        "<td>" + IdentityCard + "</td>" +
                        "<td>" + Date_Iden + "</td>" +
                        "<td>" + Place_Iden + "</td>" +
                        "<td>" + Job + "</td>" +
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
                transaction.Rollback();
            }
            return Redirect("/OrderNotice/Index");

        }
    }
}