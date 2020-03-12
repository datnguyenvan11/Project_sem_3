 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<MedicalInsurance> MedicalInsurances { get; set; }
    }
}