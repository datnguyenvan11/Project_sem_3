using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Project_sem_3.Models
{
    public class MotorInsurance
    {
        [Key, Column(Order = 0)]
        public int InsurancePackageId { get; set; }
        [Key, Column(Order = 1)]
        public int ContractId { get; set; }
        public string CarOwner { get; set; }
        public string RegisteredAddress { get; set; }
        public string LicensePlate { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime Duration { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual InsurancePackage InsurancePackage { get; set; }


    }
}