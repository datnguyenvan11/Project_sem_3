using Microsoft.AspNet.Identity;
using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var insurancePackages = db.InsurancePackages.Where(i=>i.InsuranceId==16).Where(i=>i.Status==1);
            ViewBag.shoppingCart = LoadShoppingCart();
            ViewBag.insurancepackages = insurancePackages;
            return View();
        }
        private static string SHOPPING_CART_NAME = "shoppingCart";
        // GET: ShoppingCart

        //public ActionResult Add()
        //{

        //var insurancePackages = db.InsurancePackages.Where(i=>i.InsuranceId==2);
        //return View(insurancePackages);
        //}
        public ActionResult AddCart(int insurancePackageId, int quantity, string CarOwner, string RegisteredAddress, string LicensePlate, string ChassisNumber, DateTime Duration)
        {

            // Check số lượng có hợp lệ không?
            if (quantity <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Quantity");
            }
            // Check sản phẩm có hợp lệ không?
            var insurancePackage = db.InsurancePackages.Find(insurancePackageId);
            if (insurancePackage == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Product's' not found");
            }
            // Lấy thông tin shopping cart từ session.
            var sc = LoadShoppingCart();
            // Thêm sản phẩm vào shopping cart.
            sc.AddCart(insurancePackage, quantity, CarOwner, RegisteredAddress, LicensePlate, ChassisNumber, Duration);
            // lưu thông tin cart vào session.
            SaveShoppingCart(sc);
            return Redirect("/MotorInsurance/Order");
        }


        public ActionResult RemoveCart(string LicensePlate)
        {
            // Lấy thông tin shopping cart từ session.
            var sc = LoadShoppingCart();
            // Thêm sản phẩm vào shopping cart.
            sc.RemoveFromCart(LicensePlate);
            // lưu thông tin cart vào session.
            SaveShoppingCart(sc);
            return Redirect("/MotorInsurance/Order");
        }
        [HttpGet]
        public ActionResult CreateContract()
        {
            // load cart trong session.
            var shoppingCart = LoadShoppingCart();
            if (shoppingCart.GetCartItems().Count <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad request");
            }
            // chuyển thông tin shopping cart thành Order.

            var contract = new Contract
            {
                TotalPrice = shoppingCart.GetTotalPrice(),
                ApplicationUserId =User.Identity.GetUserId(),
                InsuranceId = 16,
                MotorInsurances = new List<MotorInsurance>()
            };

            foreach (var cartItem in shoppingCart.GetCartItems())
            {
                var motorinsurance = new MotorInsurance()
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
                contract.MotorInsurances.Add(motorinsurance);
                db.Contracts.Add(contract);
            }

            db.SaveChanges();
            ClearCart();
            // lưu vào database.
            var transaction = db.Database.BeginTransaction();
            try
            {

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
            }
            return Redirect("/Home");
        }

        private void ClearCart()
        {
            Session.Remove(SHOPPING_CART_NAME);
        }

        /**
         * Tham số nhận vào là một đối tượng shopping cart.
         * Hàm sẽ lưu đối tượng vào session với key được define từ trước.
         */
        private void SaveShoppingCart(MotorShopingCart shoppingCart)
        {
            Session[SHOPPING_CART_NAME] = shoppingCart;
        }

        /**
         * Lấy thông tin shopping cart từ trong session ra. Trong trường hợp không tồn tại
         * trong session thì tạo mới đối tượng shopping cart.
         */
        private MotorShopingCart LoadShoppingCart()
        {
            // lấy thông tin giỏ hàng ra.
            if (!(Session[SHOPPING_CART_NAME] is MotorShopingCart sc))
            {
                sc = new MotorShopingCart();
            }
            return sc;
        }

    }
}