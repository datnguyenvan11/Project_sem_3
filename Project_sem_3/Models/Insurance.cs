using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_sem_3.Models
{
    public class Insurance
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public int Status { get; set; }
        public enum InsuranceStatus { Deleted = -1, Action = 0 }
        public virtual ICollection<InsurancePackage> InsurancePackages { get; set; }

    }
}