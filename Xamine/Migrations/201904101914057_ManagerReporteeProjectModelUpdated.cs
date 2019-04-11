namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerReporteeProjectModelUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.project", "ManagerProjectModel_EmpId", "dbo.manager_project");
            DropForeignKey("dbo.reportee_project", "Project_ProjectId", "dbo.project");
            DropIndex("dbo.project", new[] { "ManagerProjectModel_EmpId" });
            DropIndex("dbo.reportee_project", new[] { "Project_ProjectId" });
            AddColumn("dbo.reportee", "ProjectId", c => c.String());
            AddColumn("dbo.reportee", "HoursAssigned", c => c.Int(nullable: false));
            AddColumn("dbo.reportee", "HoursWorked", c => c.Int(nullable: false));
            AddColumn("dbo.reportee", "TaskAssigned", c => c.String());
            AddColumn("dbo.reportee", "TaskPriority", c => c.String());
            DropColumn("dbo.project", "ManagerProjectModel_EmpId");
            DropTable("dbo.manager_project");
            DropTable("dbo.reportee_project");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.reportee_project",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 128),
                        HoursAssigned = c.Int(nullable: false),
                        HoursWorked = c.Int(nullable: false),
                        TaskAssigned = c.String(),
                        TaskPriority = c.String(),
                        Project_ProjectId = c.String(nullable: false, maxLength: 7),
                    })
                .PrimaryKey(t => t.EmpId);
            
            CreateTable(
                "dbo.manager_project",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmpId);
            
            AddColumn("dbo.project", "ManagerProjectModel_EmpId", c => c.String(maxLength: 128));
            DropColumn("dbo.reportee", "TaskPriority");
            DropColumn("dbo.reportee", "TaskAssigned");
            DropColumn("dbo.reportee", "HoursWorked");
            DropColumn("dbo.reportee", "HoursAssigned");
            DropColumn("dbo.reportee", "ProjectId");
            CreateIndex("dbo.reportee_project", "Project_ProjectId");
            CreateIndex("dbo.project", "ManagerProjectModel_EmpId");
            AddForeignKey("dbo.reportee_project", "Project_ProjectId", "dbo.project", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.project", "ManagerProjectModel_EmpId", "dbo.manager_project", "EmpId");
        }
    }
}
