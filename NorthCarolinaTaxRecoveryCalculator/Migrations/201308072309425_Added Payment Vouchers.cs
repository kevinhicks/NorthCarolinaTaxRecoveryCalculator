namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPaymentVouchers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentVouchers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CheckNumber = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PaidTo = c.String(nullable: false),
                        PreparedBy = c.String(),
                        ApprovedBy = c.String(),
                        RBCApproval = c.String(),
                        Project_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.PaymentVoucherEntries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PaymentVoucherID = c.Guid(nullable: false),
                        Item = c.String(),
                        CostElement = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PaymentVouchers", t => t.PaymentVoucherID, cascadeDelete: true)
                .Index(t => t.PaymentVoucherID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PaymentVoucherEntries", new[] { "PaymentVoucherID" });
            DropIndex("dbo.PaymentVouchers", new[] { "Project_ID" });
            DropForeignKey("dbo.PaymentVoucherEntries", "PaymentVoucherID", "dbo.PaymentVouchers");
            DropForeignKey("dbo.PaymentVouchers", "Project_ID", "dbo.Projects");
            DropTable("dbo.PaymentVoucherEntries");
            DropTable("dbo.PaymentVouchers");
        }
    }
}
