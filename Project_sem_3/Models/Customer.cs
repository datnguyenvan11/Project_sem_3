using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_sem_3.Models
{
    public class Customer : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

    }
}