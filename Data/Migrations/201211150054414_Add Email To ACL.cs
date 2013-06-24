namespace Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToACL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsersAccessProjects", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsersAccessProjects", "Email");
        }
    }
}
