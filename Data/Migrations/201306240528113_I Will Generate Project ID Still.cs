namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IWillGenerateProjectIDStill : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "ID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "ID", c => c.Guid(nullable: false, identity: true));
        }
    }
}
