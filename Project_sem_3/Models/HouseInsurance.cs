using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class HouseInsurance
    {
        //[Key]
        public int Id { get; set; }
        public int InsurancePackageId { get; set; }
        public int ContractId { get; set; }
        public string HouseType { get; set; }
        public string DurationHouse { get; set; }
        public string HouseOwner { get; set; }
        public string HouserAddress { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual InsurancePackage  InsurancePackage { get; set; }


    }
}