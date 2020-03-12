using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Controllers
{
    public class AccoutRoleController : Controller
    {
        // GET: AccoutRole
        private MyDb dbContext = new MyDb();
        private RoleManager<AccountRole> roleManager;

        public AccoutRoleController()
        {
            RoleStore<AccountRole> roleStore = new RoleStore<AccountRole>(dbContext);
            roleManager = new RoleManager<AccountRole>(roleStore);
        }
        // GET: AccountRoles
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(string name)
        {
            var role = new AccountRole()
            {
                Name = name,
            };
            var result = roleManager.Create(role);
            return View("Create");
        }
    }
}