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

namespace Project_sem_3.Areas.Admin.Controllers
{
    public class ContractsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        // GET: Admin/Contracts
        public ActionResult Index(string sortOrder, string searchString, int? page, int? pageSize, string listcontracts, DateTime? startDate, DateTime? endDate)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance);
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

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }

            if(startDate != null && endDate != null)
            {
                contracts = contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x =>x.CreatedAt >= startDate && x.CreatedAt <= endDate);
            }
           
            switch (listcontracts)
            {
                case "1":
                //    var data = db.Contracts.Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate).OrderByDescending(c => c.CreatedAt).GroupBy(x => x.CreatedAt,
                //(key, values) => new {
                //    Day = key,
                //    Total = values.Sum(x => (double?)x.TotalPrice) ?? 0
                //}).ToList();
                    contracts = contracts.Where(x => x.Status == 1);
                    break;
                case "2":
                    //contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 2);
                    contracts = contracts.Where(x => x.Status == 2);
                    break;
                case "3":
                    //contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 3);
                    //double totalprice = db.Contracts.Where(c => c.Status == 3).Sum(c => (double?)(c.TotalPrice)) ?? 0;
                    contracts = contracts.Where(x => x.Status == 3);
                    double totalprice = contracts.Sum(c => (double?)(c.TotalPrice)) ?? 0;
                    ViewBag.totalprice = totalprice;
                    break;
                case "0":
                    //contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 0);
                    contracts = contracts.Where(x => x.Status == 0);
                    break;
                case "-1":
                    //contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == -1);
                    contracts = contracts.Where(x => x.Status == -1);
                    break;
                case "4":
                    contracts = db.Contracts;
                    break;
                default:
                    //contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 1);
                    //contracts = contracts;
                    break;
            }

            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Contracts/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StatusOrder(int action, int[] selectedIDs)
        {
            foreach (int IDs in selectedIDs)
            {
                Contract contract = db.Contracts.Find(IDs);
                db.Contracts.Attach(contract);
                contract.Status = action;
                var motor = db.MotorInsurances.Where(x => x.ContractId == contract.Id).ToList();
                foreach (var mt in motor)
                {
                    var ms = mt.InsurancePackage.Name;
                }
                await UserManager.SendEmailAsync(contract.ApplicationUserId,
                    "Congratulation: You have successfully created order",
                     "<b> Your order has been is : </b> " + contract.Insurance.Name
                       + "<br>" + "<b> With a total price: </b>" + contract.TotalPrice + "$"
                       + "<br>" + "<b> Total products are: </b>"
                       + "<br>" + "<b>We will ship your order in the next 2 - 3 days. </b>"
                       + "<br>" + "<b>Any questions please contact the hotline : +1 823 424 9134.<b>");

            }
            db.SaveChanges();
            return Json(selectedIDs, JsonRequestBehavior.AllowGet);
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

        public ActionResult Deleted(string sortOrder, string searchString, int? page, int? pageSize)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == -1);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    contracts = db.Contracts.Where(x => x.TotalPrice.Contains(searchString))
            //        .Where(t => t.Status == -1);
            //}
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }
            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }

        public ActionResult Confirmed(string sortOrder, string searchString, int? page, int? pageSize)
        {

            var contracts = db.Contracts.Include(c => c.ApplicationUser).Include(c => c.Insurance).Where(x => x.Status == 0);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    contracts = db.Contracts.Where(x => x.TotalPrice.Contains(searchString))
            //        .Where(t => t.Status == 0);
            //}
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Text="5", Value= "5"},
                new SelectListItem() { Text="10", Value= "10"},
                new SelectListItem() { Text="15", Value= "15" },
                new SelectListItem() { Text="20", Value= "20" },
            };
            int pagesize = (pageSize ?? 5);
            int pageNumber = (page ?? 1);

            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "TotalPrice_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    contracts = contracts.OrderBy(s => s.CreatedAt);
                    break;
                case "Date_desc":
                    contracts = contracts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "TotalPrice_desc":
                    contracts = contracts.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.TotalPrice);
                    break;
            }
            return View(contracts.ToList().ToPagedList(pageNumber, pagesize));
        }
    }
}
