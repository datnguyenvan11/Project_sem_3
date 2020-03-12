using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class MotorCartItem
    {
        public int InsurancePackageId { get; set; }
        public string InsurancePackageName { get; set; }
        public string CarOwner { get; set; }
        public string RegisteredAddress { get; set; }
        public string LicensePlate { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime Duration { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}