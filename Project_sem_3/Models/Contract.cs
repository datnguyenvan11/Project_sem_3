using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public int InsuranceId { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public long DeletedAt { get; set; }
        public int Status { get; set; }
        public enum OrderStatus { Pending = 5, Confirmed = 4, Shipping = 3, Paid = 2, Done = 1, Cancel = 0, Deleted = -1 }
        public Contract()
        {
            CreatedAt = DateTime.Now.Millisecond;
            UpdatedAt = DateTime.Now.Millisecond;
            Status = (int)OrderStatus.Pending;
        }
        public virtual ICollection<MotorInsurance> MotorInsurances { get; set; }
        public virtual ICollection<HouseInsurance> HouseInsurances { get; set; }
        public virtual ICollection<MedicalInsurance> MedicalInsurances { get; set; }
        public virtual ICollection<LifeInsurance> LifeInsurances { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Insurance Insurance { get; set; }


    }
}