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
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string DurationContract { get; set; }
        public string DurationPay { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public virtual ICollection<MotorInsurance> MotorInsurances { get; set; }
        public virtual ICollection<HouseInsurance> HouseInsurances { get; set; }
        public virtual ICollection<LifeInsurance> LifeInsurances { get; set; }
        public virtual ICollection<MedicalInsurance> MedicalInsurances { get; set; }


    }
}