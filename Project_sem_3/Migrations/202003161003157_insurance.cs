namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurance : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
    }
}
