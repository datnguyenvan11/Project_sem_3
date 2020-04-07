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

namespace Project_sem_3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationRoleManager _rolenManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db = new ApplicationDbContext();

      
        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _rolenManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _rolenManager = value;
            }
        }
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