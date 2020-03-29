using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_sem_3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("FullName", this.FullName));
            return userIdentity;
        }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base(){}
        public ApplicationRole( string roleName):base(roleName) {}
}
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyDbContext", throwIfV1Schema: false)
        {
        }
        public DbSet<InsurancePackage> InsurancePackages { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<MedicalInsurance> MedicalInsurances { get; set; }
        public DbSet<HouseInsurance> HouseInsurances { get; set; }
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<MotorInsurance> MotorInsurances { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}