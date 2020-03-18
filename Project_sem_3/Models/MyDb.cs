namespace Project_sem_3.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDb : IdentityDbContext<Customer>
    {
      

        public MyDb() : base("name=MyDbContext")
        {
        }

    

         public virtual DbSet<MyEntity> MyEntities { get; set; }
    public DbSet<InsurancePackage> InsurancePackages { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<MedicalInsurance> HealthInsurances { get; set; }
    public DbSet<HouseInsurance> HouseInsurances { get; set; }
    public DbSet<LifeInsurance> LifeInsurances { get; set; }
    public DbSet<Programme> Programmes { get; set; }



    public class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static MyDb Create()
    {
        return new MyDb();
    }

    public System.Data.Entity.DbSet<Project_sem_3.Models.MotorInsurance> MotorInsurances { get; set; }
}
}


