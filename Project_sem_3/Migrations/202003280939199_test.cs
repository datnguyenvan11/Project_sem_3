namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Contracts", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Contracts", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            AddColumn("dbo.MedicalInsurances", "IdentityCard", c => c.String());
            AddColumn("dbo.MedicalInsurances", "DateOfIdentityCard", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalInsurances", "PlaceOfIdentityCard", c => c.String());
            AddColumn("dbo.MedicalInsurances", "Job", c => c.String());
            DropColumn("dbo.Contracts", "UserId");
            DropColumn("dbo.MedicalInsurances", "DateOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalInsurances", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "UserId", c => c.String());
            DropColumn("dbo.MedicalInsurances", "Job");
            DropColumn("dbo.MedicalInsurances", "PlaceOfIdentityCard");
            DropColumn("dbo.MedicalInsurances", "DateOfIdentityCard");
            DropColumn("dbo.MedicalInsurances", "IdentityCard");
            RenameIndex(table: "dbo.Contracts", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Contracts", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
