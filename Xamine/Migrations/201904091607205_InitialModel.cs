namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.admin",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 7),
                        Fname = c.String(nullable: false, maxLength: 255),
                        Lname = c.String(maxLength: 255),
                        Contact = c.Long(nullable: false),
                        Gender = c.String(maxLength: 20),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.EmpId);
            
            CreateTable(
                "dbo.manager",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 7),
                        Fname = c.String(nullable: false, maxLength: 255),
                        Lname = c.String(maxLength: 255),
                        Contact = c.Long(nullable: false),
                        Gender = c.String(maxLength: 20),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.EmpId);
            
            CreateTable(
                "dbo.project",
                c => new
                    {
                        ProjectId = c.String(nullable: false, maxLength: 7),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 255),
                        Status = c.String(maxLength: 255),
                        Progress = c.Int(nullable: false),
                        ManagerModel_EmpId = c.String(maxLength: 7),
                        ReporteeModel_EmpId = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.manager", t => t.ManagerModel_EmpId)
                .ForeignKey("dbo.reportee", t => t.ReporteeModel_EmpId)
                .Index(t => t.ManagerModel_EmpId)
                .Index(t => t.ReporteeModel_EmpId);
            
            CreateTable(
                "dbo.reportee",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 7),
                        HoursAssigned = c.Int(nullable: false),
                        HoursWorked = c.Int(nullable: false),
                        Fname = c.String(nullable: false, maxLength: 255),
                        Lname = c.String(maxLength: 255),
                        Contact = c.Long(nullable: false),
                        Gender = c.String(maxLength: 20),
                        DOB = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.EmpId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.project", "ReporteeModel_EmpId", "dbo.reportee");
            DropForeignKey("dbo.project", "ManagerModel_EmpId", "dbo.manager");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.project", new[] { "ReporteeModel_EmpId" });
            DropIndex("dbo.project", new[] { "ManagerModel_EmpId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.reportee");
            DropTable("dbo.project");
            DropTable("dbo.manager");
            DropTable("dbo.admin");
        }
    }
}
