namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_sem_3.Models.MyDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Project_sem_3.Models.MyDb context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Insurances.AddOrUpdate(x => x.Id,
                new Models.Insurance() { Id = 1, Name = "HomeInsurance" },
                new Models.Insurance() { Id = 2, Name = "LifeInsurance" },
                new Models.Insurance() { Id = 3, Name = "MotorInsurance" },
                new Models.Insurance() { Id = 4, Name = "MedicalInsurance" }
                );
        }
    }
}
