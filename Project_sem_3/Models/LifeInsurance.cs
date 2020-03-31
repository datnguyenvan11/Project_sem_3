using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class LifeInsurance
    {
        //[Key]

        public int Id { get; set; }
        public int InsurancePackageId { get; set; }
        public int ContractId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public DateTime DateOfIdentityCard { get; set; }
        public string PlaceOfIdentityCard { get; set; }
        public string Job { get; set; }
        public string MaritalStatus { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual InsurancePackage InsurancePackage { get; set; }
    }
}