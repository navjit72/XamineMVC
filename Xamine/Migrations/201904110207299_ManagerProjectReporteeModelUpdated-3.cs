namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerProjectReporteeModelUpdated3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.project", name: "ManagerModel_EmpId", newName: "ManagerRefId");
            RenameColumn(table: "dbo.reportee", name: "Project_ProjectId", newName: "ProjectRefId");
            RenameIndex(table: "dbo.project", name: "IX_ManagerModel_EmpId", newName: "IX_ManagerRefId");
            RenameIndex(table: "dbo.reportee", name: "IX_Project_ProjectId", newName: "IX_ProjectRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.reportee", name: "IX_ProjectRefId", newName: "IX_Project_ProjectId");
            RenameIndex(table: "dbo.project", name: "IX_ManagerRefId", newName: "IX_ManagerModel_EmpId");
            RenameColumn(table: "dbo.reportee", name: "ProjectRefId", newName: "Project_ProjectId");
            RenameColumn(table: "dbo.project", name: "ManagerRefId", newName: "ManagerModel_EmpId");
        }
    }
}
