namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validataProgreme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Insurances", "Name", c => c.String());
            AlterColumn("dbo.Insurances", "Description", c => c.String());
            AlterColumn("dbo.Programmes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Programmes", "Name", c => c.String());
            AlterColumn("dbo.Insurances", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Insurances", "Name", c => c.String(nullable: false));
        }
    }
}
