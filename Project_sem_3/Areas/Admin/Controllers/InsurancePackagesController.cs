using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using PagedList;

using System.Web.Mvc;
using Project_sem_3.Models;

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class InsurancePackagesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/InsurancePackages
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            var insurancePackages = db.InsurancePackages.Include(i => i.Insurance).Where(x => x.Status == 0);
            if (!String.IsNullOrEmpty(searchString))
            {
                insurancePackages = db.InsurancePackages.Where(x => x.Name.Contains(searchString))
                    .Where(x => x.Status == 0);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            switch (sortOrder)
            {
                case "Name_desc":
                    insurancePackages = insurancePackages.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    insurancePackages = insurancePackages.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    insurancePackages = insurancePackages.OrderByDescending(s => s.CreatedAt);
                    break;
                case "Price":
                    insurancePackages = insurancePackages.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    insurancePackages = insurancePackages.OrderByDescending(s => s.Price);
                    break;
                default:
                    insurancePackages = insurancePackages.OrderBy(s => s.Name);
                    break;
            }
            return View(insurancePackages.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/InsurancePackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Create
        public ActionResult Create()
        {
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name");
            return View();
        }

        // POST: Admin/InsurancePackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InsuranceId,Name,Description,Price,DurationContract,DurationPay,CreatedAt,UpdatedAt,DeleteAt")] InsurancePackage insurancePackage)
        {
            if (ModelState.IsValid)
            {
                var insurancepackages = new InsurancePackage()
                {
                    InsuranceId = insurancePackage.InsuranceId,
                    Name = insurancePackage.Name,
                    Description = insurancePackage.Description,
                    Price = insurancePackage.Price,
                    DurationContract = insurancePackage.DurationContract,
                    DurationPay = insurancePackage.DurationPay,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeleteAt = DateTime.Now,
                };
                db.InsurancePackages.Add(insurancepackages);
                db.SaveChanges();
                var Users = db.Users.Include(u => u.Roles).ToList();

                foreach (var user in Users)
                {
                    var senderemail = new MailAddress("nguyenvandatvtacl16@gmail.com", "test");

                    var receivermail = new MailAddress(user.Email, "test");
                    var passwordemail = "vtacl123";
                    var subject = "hehe";
                    var body = insurancePackage.Name + insurancePackage.Description + insurancePackage.Price;
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
                        Subject = subject,
                        Body = body
                    }
                    )
                    {
                        smtp.Send(mess);
                    };
                }

                return RedirectToAction("Index");
            }

            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // POST: Admin/InsurancePackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InsuranceId,Name,Description,Price,DurationContract,DurationPay,CreatedAt,UpdatedAt,DeleteAt")] InsurancePackage insurancePackage)
        {
            if (ModelState.IsValid)
            {
                var insurancepackages = new InsurancePackage()
                {
                    InsuranceId = insurancePackage.InsuranceId,
                    Id = insurancePackage.Id,
                    Name = insurancePackage.Name,
                    Description = insurancePackage.Description,
                    Price = insurancePackage.Price,
                    DurationContract = insurancePackage.DurationContract,
                    DurationPay = insurancePackage.DurationPay,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeleteAt = DateTime.Now,
                };
                db.Entry(insurancepackages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InsuranceId = new SelectList(db.Insurances, "Id", "Name", insurancePackage.InsuranceId);
            return View(insurancePackage);
        }

        // GET: Admin/InsurancePackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            if (insurancePackage == null)
            {
                return HttpNotFound();
            }
            return View(insurancePackage);
        }

        // POST: Admin/InsurancePackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InsurancePackage insurancePackage = db.InsurancePackages.Find(id);
            db.InsurancePackages.Remove(insurancePackage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                InsurancePackage insurancePackage = db.InsurancePackages.Find(IDs);
                db.InsurancePackages.Attach(insurancePackage);
                insurancePackage.Status = action;
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
        public ActionResult Deleted()
        {

            return View(db.InsurancePackages.Where(t => t.Status == -1).ToList());
        }
    }
}
