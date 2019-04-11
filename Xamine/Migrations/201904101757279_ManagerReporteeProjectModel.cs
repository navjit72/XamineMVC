namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerReporteeProjectModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.project", "ManagerModel_EmpId", "dbo.manager");
            DropForeignKey("dbo.project", "ReporteeModel_EmpId", "dbo.reportee");
            DropIndex("dbo.project", new[] { "ManagerModel_EmpId" });
            DropIndex("dbo.project", new[] { "ReporteeModel_EmpId" });
            CreateTable(
                "dbo.manager_project",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmpId);
            
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
                .PrimaryKey(t => t.EmpId)
                .ForeignKey("dbo.project", t => t.Project_ProjectId, cascadeDelete: true)
                .Index(t => t.Project_ProjectId);
            
            AddColumn("dbo.project", "ManagerProjectModel_EmpId", c => c.String(maxLength: 128));
            CreateIndex("dbo.project", "ManagerProjectModel_EmpId");
            AddForeignKey("dbo.project", "ManagerProjectModel_EmpId", "dbo.manager_project", "EmpId");
            DropColumn("dbo.project", "ManagerModel_EmpId");
            DropColumn("dbo.project", "ReporteeModel_EmpId");
            DropColumn("dbo.reportee", "HoursAssigned");
            DropColumn("dbo.reportee", "HoursWorked");
            DropColumn("dbo.reportee", "TaskAssigned");
            DropColumn("dbo.reportee", "TaskPriority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.reportee", "TaskPriority", c => c.String());
            AddColumn("dbo.reportee", "TaskAssigned", c => c.String());
            AddColumn("dbo.reportee", "HoursWorked", c => c.Int(nullable: false));
            AddColumn("dbo.reportee", "HoursAssigned", c => c.Int(nullable: false));
            AddColumn("dbo.project", "ReporteeModel_EmpId", c => c.String(maxLength: 7));
            AddColumn("dbo.project", "ManagerModel_EmpId", c => c.String(maxLength: 7));
            DropForeignKey("dbo.reportee_project", "Project_ProjectId", "dbo.project");
            DropForeignKey("dbo.project", "ManagerProjectModel_EmpId", "dbo.manager_project");
            DropIndex("dbo.reportee_project", new[] { "Project_ProjectId" });
            DropIndex("dbo.project", new[] { "ManagerProjectModel_EmpId" });
            DropColumn("dbo.project", "ManagerProjectModel_EmpId");
            DropTable("dbo.reportee_project");
            DropTable("dbo.manager_project");
            CreateIndex("dbo.project", "ReporteeModel_EmpId");
            CreateIndex("dbo.project", "ManagerModel_EmpId");
            AddForeignKey("dbo.project", "ReporteeModel_EmpId", "dbo.reportee", "EmpId");
            AddForeignKey("dbo.project", "ManagerModel_EmpId", "dbo.manager", "EmpId");
        }
    }
}
