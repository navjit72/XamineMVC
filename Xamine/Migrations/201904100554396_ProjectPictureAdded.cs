namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectPictureAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.project", "ProjectPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.project", "ProjectPicture");
        }
    }
}
