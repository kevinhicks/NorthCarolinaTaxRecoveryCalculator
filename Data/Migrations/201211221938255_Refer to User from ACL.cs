namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefertoUserfromACL : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.UsersAccessProjects", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UsersAccessProjects", new[] { "UserID" });
            DropForeignKey("dbo.UsersAccessProjects", "UserID", "dbo.UserProfile");
        }
    }
}
