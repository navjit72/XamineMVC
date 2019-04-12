namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskDetailsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.reportee", "TaskAssigned", c => c.String());
            AddColumn("dbo.reportee", "TaskPriority", c => c.String());
            DropColumn("dbo.project", "ProjectPicture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.project", "ProjectPicture", c => c.String());
            DropColumn("dbo.reportee", "TaskPriority");
            DropColumn("dbo.reportee", "TaskAssigned");
        }
    }
}
