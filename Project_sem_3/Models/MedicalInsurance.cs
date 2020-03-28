using Project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class MedicalInsurance
    {
        [Key, Column(Order = 0)]
        public int InsurancePackageId { get; set; }
        [Key, Column(Order = 1)]
        public int ContractId { get; set; }
        public int ProgrammeId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
   
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Programme Programme { get; set; }
        public virtual InsurancePackage InsurancePackage { get; set; }
    }
}