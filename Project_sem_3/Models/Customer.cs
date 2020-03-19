using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class Customer:IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Status { get; set; }
        public enum OrderStatus { Done = 1, Cancel = 0, Deleted = -1 }
        public Customer()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DeletedAt = DateTime.Now;
            Status = (int)OrderStatus.Done;
        }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}