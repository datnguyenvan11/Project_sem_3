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
            context.Programmes.AddOrUpdate(x => x.Id,
               new Models.Programme() { Id = 1, Name = "Gold", Price = 200000 },
               new Models.Programme() { Id = 2, Name = "Silver", Price = 180000 },
               new Models.Programme() { Id = 3, Name = "Standard", Price = 100000},
               new Models.Programme() { Id = 4, Name = "Diamond", Price = 300000 }
               );
        }
    }
}
