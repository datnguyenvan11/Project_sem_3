using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class InsurancePackage
    {
        public int Id { get; set; }
        public int InsuranceId { get; set; }
        public virtual Insurance Insurance { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public double Price { get; set; }
        [Required]
        public string DurationContract { get; set; }
        [Required]
        public string DurationPay { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime UpdatedAt { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime DeleteAt { get; set; }
        public int Status { get; set; }
        public enum InsurancePackageStatus { Deleted = -1, Action = 0 }
        public virtual ICollection<MotorInsurance> MotorInsurances { get; set; }
        public virtual ICollection<HouseInsurance> HouseInsurances { get; set; }
        public virtual ICollection<LifeInsurance> LifeInsurances { get; set; }
        public virtual ICollection<MedicalInsurance> MedicalInsurances { get; set; }


    }
}