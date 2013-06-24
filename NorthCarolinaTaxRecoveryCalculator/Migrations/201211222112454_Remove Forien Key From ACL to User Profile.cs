namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForienKeyFromACLtoUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile");
            DropIndex("dbo.UsersAccessProjects", new[] { "UserID" });
            AlterColumn("dbo.UsersAccessProjects", "UserID", c => c.Int());
            AddForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.UsersAccessProjects", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UsersAccessProjects", new[] { "UserID" });
            DropForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile");
            AlterColumn("dbo.UsersAccessProjects", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.UsersAccessProjects", "UserID");
            AddForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
