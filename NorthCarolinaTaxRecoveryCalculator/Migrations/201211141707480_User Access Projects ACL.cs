namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAccessProjectsACL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersAccessProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Guid(nullable: false),
                        UserID = c.Int(nullable: false),
                        invitationAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersAccessProjects");
        }
    }
}
