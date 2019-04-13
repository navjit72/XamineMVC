namespace Xamine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordRulesUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.admin", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.manager", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.reportee", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.reportee", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.manager", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.admin", "Password", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
