//using System;
//using System.Globalization;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Project_sem_3.App_Start;
//using Project_sem_3.Models;

//namespace Project_sem_3.Controllers
//{
   
//    public class AccountsController : Controller
//    {
//        private MyDb _dbContext;
//        private MyUserManager _userManager;

//        public MyUserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
//            }
//            private set
//            {
//                _userManager = value;
//            }
//        }
//        public AccountsController()
//        {
//        }

//        public MyDb DbContext
//        {
//            get
//            {
//                return _dbContext ?? HttpContext.GetOwinContext().GetUserManager<MyDb>();

//            }
//            set { _dbContext = value; }
//        }
//        public ActionResult Index(string[] ids, string[] roleNames)
//        {
//            foreach (var id in ids)
//            {
//                UserManager.AddToRoles(id, roleNames);
//            }
//            Customer acc = DbContext.Users.Find("");
//            return View("Register");
//        }
//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Login(string email, string password)
//        {
//            Customer account = UserManager.Find(email, password);
//            if (account == null)
//            {
//                return HttpNotFound();
//            }
//            // success
//            var ident = UserManager.CreateIdentity(account, DefaultAuthenticationTypes.ApplicationCookie);
//            //use the instance that has been created. 
//            var authManager = HttpContext.GetOwinContext().Authentication;
//            authManager.SignIn(
//                new AuthenticationProperties { IsPersistent = false }, ident);
//            return Redirect("/Home");
//        }


//        // GET: Accounts
//        public ActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult> Store(string Name, string Password ,string Email,string Address,string PhoneNumber,string Gender)
//        {
//            var customer = new Customer()
//            {
//                Name = Name,
//                Email = Email,
//                Password = Password,
//                Address = Address,
//                Gender="1",
//                PhoneNumber=PhoneNumber,
//                UpdatedAt= DateTime.Now,
//                CreatedAt = DateTime.Now
//            };
//            IdentityResult result = await UserManager.CreateAsync(customer, Password);
//            if (result.Succeeded)
//            {
//                UserManager.AddToRole(customer.Id, "User");

//                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
//                // Send an email with this link
//                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(account.Id);
//                await UserManager.SendEmailAsync(customer.Id, "Hello world! Please confirm your account", "<b>Please confirm your account</b> by clicking <a href=\"http://google.com.vn\">here</a>");
//                return RedirectToAction("Index", "Home");
//            }

//            return View("Register");
//        }

//        [Authorize]
//        [HttpPost]
//        public ActionResult Logout()
//        {
//            HttpContext.GetOwinContext().Authentication.SignOut();
//            return Redirect("/Home");
//        }
//    }
//}