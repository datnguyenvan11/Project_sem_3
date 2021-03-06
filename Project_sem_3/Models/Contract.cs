﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class Contract
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public double TotalPrice { get; set; }
        public int InsuranceId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeletedAt { get; set; }
        public int Status { get; set; }
        public enum OrderStatus { Done = 3, Pay = 2, Pending = 1, Confirmed = 0, Deleted = -1 }
        public Contract()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DeletedAt = DateTime.Now;
            Status = (int)OrderStatus.Pending;
        }
        public virtual ICollection<MotorInsurance> MotorInsurances { get; set; }
        public virtual ICollection<HouseInsurance> HouseInsurances { get; set; }
        public virtual ICollection<MedicalInsurance> MedicalInsurances { get; set; }
        public virtual ICollection<LifeInsurance> LifeInsurances { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Insurance Insurance { get; set; }


    }
}