using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Security;
using Project_sem_3.App_Start;
using Project_sem_3.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;

namespace Project_sem_3.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private ApplicationRoleManager _rolenManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

      
    

      
        // GET: Admin/Admin
        public ActionResult Index()
        {
            var contracts = db.Contracts.Include(c => c.User).Include(c => c.Insurance);
            contracts = contracts.Where(x => x.Status == 3);
            double totalprice = contracts.Sum(c => (double?)(c.TotalPrice)) ?? 0;
            ViewBag.totalprice = totalprice;

            int count = (from row in db.Contracts
                where row.Status == 1
                select row).Count();

            ViewBag.totalpending = count;

            int contract_done = (from row in db.Contracts
                where row.Status == 3
                select row).Count();

            ViewBag.totaldone = contract_done;


          

            return View();
        }

        public ActionResult Table()
        {
            return View();
        }

      

    }
}