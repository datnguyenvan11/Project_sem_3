using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using Project_sem_3.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Project_sem_3.App_Start;
using Microsoft.AspNet.Identity.Owin;
using System.Threading;
using System.Net.Mail;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class ContractsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Contracts
        public ActionResult Index(string sortOrder, string searchString, int? page, int? pageSize, string listcontracts, DateTime? startDate, DateTime? endDate)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where( c => c.Status != 5);
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };

            ViewBag.listcontracts = new List<SelectListItem>()
            {

                new SelectListItem() { Text="All", Value= "4" },
                new SelectListItem() { Text="Confirmed", Value= "0"},

                new SelectListItem() { Text="Pending", Value= "1" },
                new SelectListItem() { Text="Pay", Value= "2" },
                new SelectListItem() { Text="Deleted", Value= "-1"},
                new SelectListItem() { Text="Done", Value= "3" },

            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.psize = pagesize;


            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt).Where(c => c.Status != 5);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt).Where(c => c.Status != 5); 
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice).Where(c => c.Status != 5); 
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice).Where(c => c.Status != 5); 
                    break;
            }

            if (startDate != null && endDate != null)
            {
                contracts = contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
            }

            switch (listcontracts)
            {
                case "1":
                    contracts = contracts.Where(x => x.Status == 1);
                    break;
                case "2":
                    contracts = contracts.Where(x => x.Status == 2);
                    break;
                case "3":
                    contracts = contracts.Where(x => x.Status == 3);
                    double totalprice = contracts.Sum(c => (double?)(c.TotalPrice)) ?? 0;
                    ViewBag.totalprice = totalprice;
                    break;
                case "0":
                    contracts = contracts.Where(x => x.Status == 0);
                    break;
                case "-1":
                    contracts = contracts.Where(x => x.Status == -1);
                    break;
                default:
                    break;
            }

            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Contract contract = db.Contracts.Find(IDs);
                db.Contracts.Attach(contract);
                contract.Status = action;
                    if (action == 0)
                    {
                        // HouseInsurance
                        if (contract.InsuranceId == 1)
                        {
                            var homeInsurance = db.HouseInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                            body += "<p>" + "Your contract has been confirmed, we will get back to you!" + " </p>"
                             + "<p>" + "Name Insurance : HouseInsurance</p>"
                             + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                            "<br>";
                            body += "</HEAD><BODY>" +
                                "<tr>" +
                                "<th>ContractId</th>" +
                                "<th>Package Insurance</th>" +
                                "<th>HouseOwner</th>" +
                                "<th>HouseType</th>" +
                                "<th>HouserAddress</th>" +
                                "<th>DurationHouse</th>" +
                                "<th>Quantity</th>" +
                                "<th>UnitPrice</th>" +
                                "</tr>";
                            foreach (var data in homeInsurance)
                            {
                                body +=
                                    "<tr>" +
                                    "<td>" + contract.Id + "</td>" +
                                    "<td>" + e.Name + "</td>" +
                                    "<td>" + data.HouseOwner + "</td>" +
                                    "<td>" + data.HouseType + "</td>" +
                                    "<td>" + data.HouserAddress + "</td>" +
                                    "<td>" + data.DurationHouse + "</td>" +
                                    "<td>" + 1 + "</td>" +
                                    "<td>" + e.Price + "</td>" +
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
                        }

                        // LifeInsurance
                        if (contract.InsuranceId == 2)
                        {
                            var lifeInsurance = db.LifeInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                            body += "<p>" + "Your contract has been confirmed, we will get back to you!" + " </p>"
                             + "<p>" + "Name Insurance : LifeInsurance</p>"
                             + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                            "<br>";
                            body += "</HEAD><BODY>" +
                                "<tr>" +
                                "<th>ContractId</th>" +
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
                            foreach (var data in lifeInsurance)
                            {
                                body +=
                                    "<tr>" +
                                    "<td>" + contract.Id + "</td>" +
                                    "<td>" + e.Name + "</td>" +
                                    "<td>" + data.Name + "</td>" +
                                    "<td>" + data.PhoneNumber + "</td>" +
                                    "<td>" + data.Email + "</td>" +
                                    "<td>" + data.Address + "</td>" +
                                    "<td>" + data.IdentityCard + "</td>" +
                                    "<td>" + data.DateOfIdentityCard + "</td>" +
                                    "<td>" + data.PlaceOfIdentityCard + "</td>" +
                                    "<td>" + data.Job + "</td>" +
                                    "<td>" + 1 + "</td>" +
                                    "<td>" + e.Price + "</td>" +
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
                        }

                        // MotorInsurance
                        if (contract.InsuranceId == 3)
                        {
                            var motorInsurance = db.MotorInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                            body += "<p>" + "Your contract has been confirmed, we will get back to you!" + " </p>"
                             + "<p>" + "Name Insurance : MotorInsurance</p>"
                             + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                            "<br>";
                            body += "</HEAD><BODY>" +
                                "<tr>" +
                                "<th>ContractId</th>" +
                                "<th>Package Insurance</th>" +
                                "<th>RegisteredAddress</th>" +
                                "<th>CarOwner</th>" +
                                "<th>ChassisNumber</th>" +
                                "<th>LicensePlate</th>" +
                                "<th>Duration</th>" +
                                "<th>Quantity</th>" +
                                "<th>UnitPrice</th>" +
                                "</tr>";
                            foreach (var data in motorInsurance)
                            {
                                body +=
                                    "<tr>" +
                                    "<td>" + contract.Id + "</td>" +
                                    "<td>" + e.Name + "</td>" +
                                    "<td>" + data.RegisteredAddress + "</td>" +
                                    "<td>" + data.CarOwner + "</td>" +
                                    "<td>" + data.ChassisNumber + "</td>" +
                                    "<td>" + data.LicensePlate + "</td>" +
                                    "<td>" + data.Duration + "</td>" +
                                    "<td>" + 1 + "</td>" +
                                    "<td>" + e.Price + "</td>" +
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
                        }

                        // MedicalInsurance
                        if (contract.InsuranceId == 4)
                        {
                            var medicalInsurances = db.MedicalInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                            body += "<p>" + "Your contract has been confirmed, we will get back to you!" + " </p>"
                             + "<p>" + "Name Insurance : MedicalInsurance</p>"
                             + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                            "<br>";
                            body += "</HEAD><BODY>" +
                                "<tr>" +
                                "<th>ContractId</th>" +
                                "<th>Package Insurance</th>" +
                                "<th>Name</th>" +
                                "<th>Gender</th>" +
                                "<th>Address</th>" +
                                "<th>Quantity</th>" +
                                "<th>UnitPrice</th>" +
                                "<th>DateOfBirth</th>" +
                                "</tr>";
                            foreach (var data in medicalInsurances)
                            {
                                body +=
                                    "<tr>" +
                                    "<td>" + contract.Id + "</td>" +
                                    "<td>" + e.Name + "</td>" +
                                    "<td>" + data.Name + "</td>" +
                                    "<td>" + data.Gender + "</td>" +
                                    "<td>" + data.Address + "</td>" +
                                    "<td>" + 1 + "</td>" +
                                    "<td>" + e.Price + "</td>" +
                                    "<td>" + data.DateOfBirth + "</td>" +
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
                        }
                    }
                // 2 : Pay
                if (action == 2)
                {
                    // HouseInsurance
                    if (contract.InsuranceId == 1)
                    {
                        var homeInsurance = db.HouseInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been pay, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : HouseInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>HouseOwner</th>" +
                            "<th>HouseType</th>" +
                            "<th>HouserAddress</th>" +
                            "<th>DurationHouse</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in homeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.HouseOwner + "</td>" +
                                "<td>" + data.HouseType + "</td>" +
                                "<td>" + data.HouserAddress + "</td>" +
                                "<td>" + data.DurationHouse + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // LifeInsurance
                    if (contract.InsuranceId == 2)
                    {
                        var lifeInsurance = db.LifeInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been pay, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : LifeInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
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
                        foreach (var data in lifeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.PhoneNumber + "</td>" +
                                "<td>" + data.Email + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + data.IdentityCard + "</td>" +
                                "<td>" + data.DateOfIdentityCard + "</td>" +
                                "<td>" + data.PlaceOfIdentityCard + "</td>" +
                                "<td>" + data.Job + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MotorInsurance
                    if (contract.InsuranceId == 3)
                    {
                        var motorInsurance = db.MotorInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been pay, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MotorInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>RegisteredAddress</th>" +
                            "<th>CarOwner</th>" +
                            "<th>ChassisNumber</th>" +
                            "<th>LicensePlate</th>" +
                            "<th>Duration</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in motorInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.RegisteredAddress + "</td>" +
                                "<td>" + data.CarOwner + "</td>" +
                                "<td>" + data.ChassisNumber + "</td>" +
                                "<td>" + data.LicensePlate + "</td>" +
                                "<td>" + data.Duration + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MedicalInsurance
                    if (contract.InsuranceId == 4)
                    {
                        var medicalInsurances = db.MedicalInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been pay, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MedicalInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>Name</th>" +
                            "<th>Gender</th>" +
                            "<th>Address</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "<th>DateOfBirth</th>" +
                            "</tr>";
                        foreach (var data in medicalInsurances)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.Gender + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
                                "<td>" + data.DateOfBirth + "</td>" +
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
                    }
                }
                // 3 Done
                if (action == 3)
                {
                    // HouseInsurance
                    if (contract.InsuranceId == 1)
                    {
                        var homeInsurance = db.HouseInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been done, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : HouseInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>HouseOwner</th>" +
                            "<th>HouseType</th>" +
                            "<th>HouserAddress</th>" +
                            "<th>DurationHouse</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in homeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.HouseOwner + "</td>" +
                                "<td>" + data.HouseType + "</td>" +
                                "<td>" + data.HouserAddress + "</td>" +
                                "<td>" + data.DurationHouse + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // LifeInsurance
                    if (contract.InsuranceId == 2)
                    {
                        var lifeInsurance = db.LifeInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been done, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : LifeInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
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
                        foreach (var data in lifeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.PhoneNumber + "</td>" +
                                "<td>" + data.Email + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + data.IdentityCard + "</td>" +
                                "<td>" + data.DateOfIdentityCard + "</td>" +
                                "<td>" + data.PlaceOfIdentityCard + "</td>" +
                                "<td>" + data.Job + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MotorInsurance
                    if (contract.InsuranceId == 3)
                    {
                        var motorInsurance = db.MotorInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been done, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MotorInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>RegisteredAddress</th>" +
                            "<th>CarOwner</th>" +
                            "<th>ChassisNumber</th>" +
                            "<th>LicensePlate</th>" +
                            "<th>Duration</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in motorInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.RegisteredAddress + "</td>" +
                                "<td>" + data.CarOwner + "</td>" +
                                "<td>" + data.ChassisNumber + "</td>" +
                                "<td>" + data.LicensePlate + "</td>" +
                                "<td>" + data.Duration + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MedicalInsurance
                    if (contract.InsuranceId == 4)
                    {
                        var medicalInsurances = db.MedicalInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been done, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MedicalInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>Name</th>" +
                            "<th>Gender</th>" +
                            "<th>Address</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "<th>DateOfBirth</th>" +
                            "</tr>";
                        foreach (var data in medicalInsurances)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.Gender + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
                                "<td>" + data.DateOfBirth + "</td>" +
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
                    }
                }
                //-1: deleted
                if (action == -1)
                {
                    // HouseInsurance
                    if (contract.InsuranceId == 1)
                    {
                        var homeInsurance = db.HouseInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been deleted, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : HouseInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>HouseOwner</th>" +
                            "<th>HouseType</th>" +
                            "<th>HouserAddress</th>" +
                            "<th>DurationHouse</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in homeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.HouseOwner + "</td>" +
                                "<td>" + data.HouseType + "</td>" +
                                "<td>" + data.HouserAddress + "</td>" +
                                "<td>" + data.DurationHouse + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // LifeInsurance
                    if (contract.InsuranceId == 2)
                    {
                        var lifeInsurance = db.LifeInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been deleted, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : LifeInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
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
                        foreach (var data in lifeInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.PhoneNumber + "</td>" +
                                "<td>" + data.Email + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + data.IdentityCard + "</td>" +
                                "<td>" + data.DateOfIdentityCard + "</td>" +
                                "<td>" + data.PlaceOfIdentityCard + "</td>" +
                                "<td>" + data.Job + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MotorInsurance
                    if (contract.InsuranceId == 3)
                    {
                        var motorInsurance = db.MotorInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been deleted, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MotorInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>RegisteredAddress</th>" +
                            "<th>CarOwner</th>" +
                            "<th>ChassisNumber</th>" +
                            "<th>LicensePlate</th>" +
                            "<th>Duration</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "</tr>";
                        foreach (var data in motorInsurance)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.RegisteredAddress + "</td>" +
                                "<td>" + data.CarOwner + "</td>" +
                                "<td>" + data.ChassisNumber + "</td>" +
                                "<td>" + data.LicensePlate + "</td>" +
                                "<td>" + data.Duration + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
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
                    }

                    // MedicalInsurance
                    if (contract.InsuranceId == 4)
                    {
                        var medicalInsurances = db.MedicalInsurances.Where(x => x.ContractId == contract.Id).ToList();
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
                        body += "<p>" + "Your contract has been deleted, we will get back to you!" + " </p>"
                         + "<p>" + "Name Insurance : MedicalInsurance</p>"
                         + "<p>" + "Total Price :" + contract.TotalPrice + "$" + "</p>" +
                        "<br>";
                        body += "</HEAD><BODY>" +
                            "<tr>" +
                            "<th>ContractId</th>" +
                            "<th>Package Insurance</th>" +
                            "<th>Name</th>" +
                            "<th>Gender</th>" +
                            "<th>Address</th>" +
                            "<th>Quantity</th>" +
                            "<th>UnitPrice</th>" +
                            "<th>DateOfBirth</th>" +
                            "</tr>";
                        foreach (var data in medicalInsurances)
                        {
                            body +=
                                "<tr>" +
                                "<td>" + contract.Id + "</td>" +
                                "<td>" + e.Name + "</td>" +
                                "<td>" + data.Name + "</td>" +
                                "<td>" + data.Gender + "</td>" +
                                "<td>" + data.Address + "</td>" +
                                "<td>" + 1 + "</td>" +
                                "<td>" + e.Price + "</td>" +
                                "<td>" + data.DateOfBirth + "</td>" +
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
                    }
                }
            }
            db.SaveChanges();
            return Json(selectedIDs, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult getChartDataApi(DateTime startDate, DateTime endDate)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var data = db.Orders.Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate).OrderByDescending(o => o.CreatedAt).GroupBy(o => o.CreatedAt, (day, orders) => new{Key = day.ToString("YYYY-MM-DD"), Total = orders.Sum(o => o.TotalPrice)}).ToList();
            var Result = db.Contracts.Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate).OrderByDescending(o => o.CreatedAt).Where(o => o.Status==3).GroupBy(x => DbFunctions.TruncateTime(x.CreatedAt),
                (key, values) => new
                {
                    day = key,
                    revenue = values.Sum(x => x.TotalPrice)
                }).ToList();


            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getPieChartDataApi()
        {

            return View();

        }

    }
}
