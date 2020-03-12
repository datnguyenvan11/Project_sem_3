using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class MotorShopingCart
    {
        private Dictionary<string, MotorCartItem> _cartItems = new Dictionary<string, MotorCartItem>();
        private double _totalPrice = 0;
        public double GetTotalPrice()
        {
            this._totalPrice = 0;
            foreach (var item in _cartItems.Values)
            {
                this._totalPrice += item.Price;
            }
            return this._totalPrice;
        }

        public Dictionary<string, MotorCartItem> GetCartItems()
        {
            return _cartItems;
        }

        public void SetCartItems(Dictionary<string, MotorCartItem> cartItems)
        {
            this._cartItems = cartItems;
        }

        /**
         * Thêm một sản phẩm vào giỏ hàng.
         * Trong trường hợp tồn tại sản phẩm trong giỏ hàng thì update số lượng.
         * Trong trường hợp không tồn tại thì thêm mới.
         */
        public void AddCart(InsurancePackage insurancePackage, int quantity, string CarOwner, string RegisteredAddress, string LicensePlate, string ChassisNumber, DateTime Duration)
        {
            if (_cartItems.ContainsKey(LicensePlate))
            {
               
                return;
            }
            

            var cartItem = new MotorCartItem
            {
                InsurancePackageId = insurancePackage.Id,
                InsurancePackageName = insurancePackage.Name,
                Price = insurancePackage.Price,
                Quantity = quantity,
                CarOwner=CarOwner,
                RegisteredAddress=RegisteredAddress,
                LicensePlate=LicensePlate,
                ChassisNumber=ChassisNumber,
                Duration=Duration
            };
            // đưa cart item tương ứng với sản phẩm (ở trên) vào danh sách.
            _cartItems.Add(cartItem.LicensePlate, cartItem);
        }

      

        public void RemoveFromCart(string LicensePlate)
        {
            _cartItems.Remove(LicensePlate);
        }

    
}
}