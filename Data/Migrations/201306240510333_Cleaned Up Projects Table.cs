namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanedUpProjectsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
        }
    }
}
