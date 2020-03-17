namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InsurancePackages", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Insurances", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Programmes", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programmes", "Status");
            DropColumn("dbo.Insurances", "Status");
            DropColumn("dbo.InsurancePackages", "Status");
        }
    }
}
