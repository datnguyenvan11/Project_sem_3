namespace Project_sem_3.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyDb : IdentityDbContext<Customer>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public MyDb() : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<InsurancePackage> InsurancePackages { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<MedicalInsurance> HealthInsurances { get; set; }
        public DbSet<HouseInsurance> HouseInsurances { get; set; }
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<Programme> Programmes { get; set; }



        //public class MyEntity
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}
        public static MyDb Create()
        {
            return new MyDb();
        }
    }
}


