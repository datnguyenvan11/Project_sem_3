using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project_sem_3.App_Start;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
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
        [Authorize]
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
                    Gender = item.Gender,
                    Address = item.Address,
                    Quantity = 1,
                    UnitPrice = item.unitprice,
                    DateOfBirth = item.DateOfbirth
                };
                contract.MedicalInsurances.Add(medicalinsurace);
                db.Contracts.Add(contract);
            }
            db.SaveChanges();
            var transaction = db.Database.BeginTransaction();
            try
            {
                var em = db.Users.Where(x => x.Id == contract.ApplicationUserId).FirstOrDefault();
                var e = db.InsurancePackages.Where(x => x.InsuranceId == contract.InsuranceId).FirstOrDefault();
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
                 + "<p>" + "Name Insurance : MedicalInsurance</p>"
                 + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                "<br>";
                body += "</HEAD><BODY>" +
                    "<tr>" +
                    "<th>Package Insurance</th>" +
                    "<th>Name</th>" +
                    "<th>Gender</th>" +
                    "<th>Address</th>" +
                    "<th>Quantity</th>" +
                    "<th>UnitPrice</th>" +
                    "<th>DateOfBirth</th>" +
                    "</tr>";
                foreach (var data in medical.items)
                {
                    body +=
                        "<tr>" +
                        "<td>" + e.Name + "</td>" +
                        "<td>" + data.Name + "</td>" +
                        "<td>" + data.Gender + "</td>" +
                        "<td>" + data.Address + "</td>" +
                        "<td>" + 1 + "</td>" +
                        "<td>" + data.unitprice + "</td>" +
                        "<td>" + data.DateOfbirth + "</td>" +
                        "</tr>";
                }
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
            return Json(new {status=201 });

        }
    }
}