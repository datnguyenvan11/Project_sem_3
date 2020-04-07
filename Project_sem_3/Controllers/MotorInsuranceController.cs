using Microsoft.AspNet.Identity;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class MotorInsuranceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: MotorInsurance
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            var insurancePackages = db.InsurancePackages.Where(i=>i.InsuranceId==3).Where(i=>i.Status==1);
            ViewBag.shoppingCart = LoadShoppingCart();
            ViewBag.insurancepackages = insurancePackages;
            return View();
        }
        private static string SHOPPING_CART_NAME = "shoppingCart";
        [Authorize(Roles = "Admin")]
        public ActionResult AddCart(int insurancePackageId, int quantity, string CarOwner, string RegisteredAddress, string LicensePlate, string ChassisNumber, DateTime Duration)
        {

            if (quantity <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Quantity");
            }
            var insurancePackage = db.InsurancePackages.Find(insurancePackageId);
            if (insurancePackage == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Product's' not found");
            }
            var sc = LoadShoppingCart();
            sc.AddCart(insurancePackage, quantity, CarOwner, RegisteredAddress, LicensePlate, ChassisNumber, Duration);
            SaveShoppingCart(sc);
            return Redirect("/MotorInsurance/Order");
        }


        public ActionResult RemoveCart(string LicensePlate)
        {
            var sc = LoadShoppingCart();
            sc.RemoveFromCart(LicensePlate);
            SaveShoppingCart(sc);
            return Redirect("/MotorInsurance/Order");
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateContract()
        {
            var shoppingCart = LoadShoppingCart();
            if (shoppingCart.GetCartItems().Count <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad request");
            }
            var contract = new Contract
            {
                TotalPrice = shoppingCart.GetTotalPrice(),
                UserID =User.Identity.GetUserId(),
                InsuranceId = 3,
                MotorInsurances = new List<MotorInsurance>()
            };
            var mt = db.MotorInsurances.ToList();
            foreach (var cartItem in shoppingCart.GetCartItems())
            {
                var motorInsurance = new MotorInsurance()
                {
                    InsurancePackageId = cartItem.Value.InsurancePackageId,
                    RegisteredAddress = cartItem.Value.RegisteredAddress,
                    CarOwner = cartItem.Value.CarOwner,
                    ChassisNumber = cartItem.Value.ChassisNumber,
                    LicensePlate = cartItem.Value.LicensePlate,
                    Duration = cartItem.Value.Duration,
                    ContractId = contract.Id,
                    Quantity = cartItem.Value.Quantity,
                    UnitPrice = cartItem.Value.Price
                };
                contract.MotorInsurances.Add(motorInsurance);
                db.Contracts.Add(contract);
            }
            db.SaveChanges();
            // lưu vào database.
            var transaction = db.Database.BeginTransaction();
            try
            {
                var em = db.Users.Where(x => x.Id == contract.UserID).FirstOrDefault();
                var senderemail = new MailAddress("nguyenvandatvtacl16@gmail.com", "Insurance Company");
                var receivermail = new MailAddress(em.Email, "Insurance Company");
                var passwordemail = "vtacl123";
                var subject = "Notification Order";
                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">"+
                    "<style>table, td, th {border: 1px solid black; font-size: 15px}</style>" +
                    "<style> p {font-size: 18px}</style>"
                    ;
                body += "<p>" + "Your order information." + "</p>" 
                 + "<p>" + "Name Insurance : MotorInsurance</p>" 
                 + "<p>"+"Total Price :" + contract.TotalPrice + "$"+"</p>" +
                "<br>";
                body += "</HEAD><BODY>"+
                    "<tr>" +
                    "<th>Package Insurance</th>" +
                    "<th>RegisteredAddress</th>" +
                    "<th>CarOwner</th>" +
                    "<th>ChassisNumber</th>" +
                    "<th>LicensePlate</th>" +
                    "<th>Duration</th>" +
                    "<th>Quantity</th>" +
                    "<th>UnitPrice</th>" +
                    "</tr>";
                foreach (var item in shoppingCart.GetCartItems())
                {
                    body +=
                        "<tr>" +
                        "<td>" + item.Value.InsurancePackageName + "</td>" +
                        "<td>" + item.Value.RegisteredAddress + "</td>" +
                        "<td>" + item.Value.CarOwner + "</td>" +
                        "<td>" + item.Value.ChassisNumber + "</td>" +
                        "<td>" + item.Value.LicensePlate + "</td>" +
                        "<td>" + item.Value.Duration + "</td>" +
                        "<td>" + item.Value.Quantity + "</td>" +
                        "<td>" + item.Value.Price + "</td>" +
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
                ClearCart();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
            }
            return Redirect("/OrderNotice/Index");
        }

        private void ClearCart()
        {
            Session.Remove(SHOPPING_CART_NAME);
        }

        private void SaveShoppingCart(MotorShopingCart shoppingCart)
        {
            Session[SHOPPING_CART_NAME] = shoppingCart;
        }
        private MotorShopingCart LoadShoppingCart()
        {
            if (!(Session[SHOPPING_CART_NAME] is MotorShopingCart sc))
            {
                sc = new MotorShopingCart();
            }
            return sc;
        }

    }
}