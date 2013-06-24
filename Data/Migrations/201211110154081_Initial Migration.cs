namespace Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reciepts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProjectID = c.Guid(nullable: false),
                        RIF = c.String(nullable: false),
                        DateOfSale = c.DateTime(nullable: false),
                        StoreName = c.String(nullable: false),
                        County = c.Int(nullable: false),
                        SalesTax = c.Single(nullable: false),
                        FoodTax = c.Single(nullable: false),
                        SalesAmount = c.Single(nullable: false),
                        Notes = c.String(),
                        OnBillDetail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        OwnerID = c.Int(nullable: false),
                        Owner_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserProfile", t => t.Owner_UserId)
                .Index(t => t.Owner_UserId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "Owner_UserId" });
            DropIndex("dbo.Reciepts", new[] { "ProjectID" });
            DropForeignKey("dbo.Projects", "Owner_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Reciepts", "ProjectID", "dbo.Projects");
           // DropTable("dbo.UserProfile");
            DropTable("dbo.Projects");
            DropTable("dbo.Reciepts");
        }
    }
}
