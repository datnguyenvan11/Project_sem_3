namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tesst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        CreatedAt = c.Long(nullable: false),
                        UpdatedAt = c.Long(nullable: false),
                        DeletedAt = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.Insurances", t => t.InsuranceId, cascadeDelete: true)
                .Index(t => t.InsuranceId)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        Gender = c.Int(nullable: false),
                        CreatedAt = c.Long(nullable: false),
                        UpdatedAt = c.Long(nullable: false),
                        DeletedAt = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.HouseInsurances",
                c => new
                    {
                        InsurancePackageId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                        HouseType = c.String(),
                        DurationHouse = c.String(),
                        HouseOwner = c.String(),
                        HouserAddress = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InsurancePackageId, t.ContractId })
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.InsurancePackages", t => t.InsurancePackageId, cascadeDelete: true)
                .Index(t => t.InsurancePackageId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.InsurancePackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsuranceId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        DurationContract = c.String(),
                        DurationPay = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        DeleteAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Insurances", t => t.InsuranceId, cascadeDelete: false)
                .Index(t => t.InsuranceId);
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        DeleteAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LifeInsurances",
                c => new
                    {
                        InsurancePackageId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        IdentityCard = c.String(),
                        DateOfIdentityCard = c.DateTime(nullable: false),
                        PlaceOfIdentityCard = c.String(),
                        Job = c.String(),
                        MaritalStatus = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InsurancePackageId, t.ContractId })
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.InsurancePackages", t => t.InsurancePackageId, cascadeDelete: true)
                .Index(t => t.InsurancePackageId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.MedicalInsurances",
                c => new
                    {
                        InsurancePackageId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                        ProgrammeId = c.Int(nullable: false),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        IdentityCard = c.String(),
                        DateOfIdentityCard = c.DateTime(nullable: false),
                        PlaceOfIdentityCard = c.String(),
                        Job = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InsurancePackageId, t.ContractId })
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.InsurancePackages", t => t.InsurancePackageId, cascadeDelete: true)
                .ForeignKey("dbo.Programmes", t => t.ProgrammeId, cascadeDelete: true)
                .Index(t => t.InsurancePackageId)
                .Index(t => t.ContractId)
                .Index(t => t.ProgrammeId);
            
            CreateTable(
                "dbo.Programmes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MotorInsurances",
                c => new
                    {
                        InsurancePackageId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                        CarOwner = c.String(),
                        RegisteredAddress = c.String(),
                        LicensePlate = c.String(),
                        ChassisNumber = c.String(),
                        Duration = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InsurancePackageId, t.ContractId })
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .ForeignKey("dbo.InsurancePackages", t => t.InsurancePackageId, cascadeDelete: true)
                .Index(t => t.InsurancePackageId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Contracts", "InsuranceId", "dbo.Insurances");
            DropForeignKey("dbo.MotorInsurances", "InsurancePackageId", "dbo.InsurancePackages");
            DropForeignKey("dbo.MotorInsurances", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.MedicalInsurances", "ProgrammeId", "dbo.Programmes");
            DropForeignKey("dbo.MedicalInsurances", "InsurancePackageId", "dbo.InsurancePackages");
            DropForeignKey("dbo.MedicalInsurances", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.LifeInsurances", "InsurancePackageId", "dbo.InsurancePackages");
            DropForeignKey("dbo.LifeInsurances", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.InsurancePackages", "InsuranceId", "dbo.Insurances");
            DropForeignKey("dbo.HouseInsurances", "InsurancePackageId", "dbo.InsurancePackages");
            DropForeignKey("dbo.HouseInsurances", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MotorInsurances", new[] { "ContractId" });
            DropIndex("dbo.MotorInsurances", new[] { "InsurancePackageId" });
            DropIndex("dbo.MedicalInsurances", new[] { "ProgrammeId" });
            DropIndex("dbo.MedicalInsurances", new[] { "ContractId" });
            DropIndex("dbo.MedicalInsurances", new[] { "InsurancePackageId" });
            DropIndex("dbo.LifeInsurances", new[] { "ContractId" });
            DropIndex("dbo.LifeInsurances", new[] { "InsurancePackageId" });
            DropIndex("dbo.InsurancePackages", new[] { "InsuranceId" });
            DropIndex("dbo.HouseInsurances", new[] { "ContractId" });
            DropIndex("dbo.HouseInsurances", new[] { "InsurancePackageId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Contracts", new[] { "Customer_Id" });
            DropIndex("dbo.Contracts", new[] { "InsuranceId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MotorInsurances");
            DropTable("dbo.Programmes");
            DropTable("dbo.MedicalInsurances");
            DropTable("dbo.LifeInsurances");
            DropTable("dbo.Insurances");
            DropTable("dbo.InsurancePackages");
            DropTable("dbo.HouseInsurances");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Contracts");
        }
    }
}
