namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerReporteeProjectModelUpdated2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.project", "ManagerModel_EmpId", c => c.String(maxLength: 7));
            AddColumn("dbo.reportee", "Project_ProjectId", c => c.String(maxLength: 7));
            CreateIndex("dbo.project", "ManagerModel_EmpId");
            CreateIndex("dbo.reportee", "Project_ProjectId");
            AddForeignKey("dbo.project", "ManagerModel_EmpId", "dbo.manager", "EmpId");
            AddForeignKey("dbo.reportee", "Project_ProjectId", "dbo.project", "ProjectId");
            DropColumn("dbo.reportee", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.reportee", "ProjectId", c => c.String());
            DropForeignKey("dbo.reportee", "Project_ProjectId", "dbo.project");
            DropForeignKey("dbo.project", "ManagerModel_EmpId", "dbo.manager");
            DropIndex("dbo.reportee", new[] { "Project_ProjectId" });
            DropIndex("dbo.project", new[] { "ManagerModel_EmpId" });
            DropColumn("dbo.reportee", "Project_ProjectId");
            DropColumn("dbo.project", "ManagerModel_EmpId");
        }
    }
}
