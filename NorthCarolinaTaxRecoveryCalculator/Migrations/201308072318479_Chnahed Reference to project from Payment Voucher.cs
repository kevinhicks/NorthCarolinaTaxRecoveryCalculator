namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChnahedReferencetoprojectfromPaymentVoucher : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentVouchers", "Project_ID", "dbo.Projects");
            DropIndex("dbo.PaymentVouchers", new[] { "Project_ID" });
            RenameColumn(table: "dbo.PaymentVouchers", name: "Project_ID", newName: "ProjectID");
            AddForeignKey("dbo.PaymentVouchers", "ProjectID", "dbo.Projects", "ID", cascadeDelete: true);
            CreateIndex("dbo.PaymentVouchers", "ProjectID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PaymentVouchers", new[] { "ProjectID" });
            DropForeignKey("dbo.PaymentVouchers", "ProjectID", "dbo.Projects");
            RenameColumn(table: "dbo.PaymentVouchers", name: "ProjectID", newName: "Project_ID");
            CreateIndex("dbo.PaymentVouchers", "Project_ID");
            AddForeignKey("dbo.PaymentVouchers", "Project_ID", "dbo.Projects", "ID");
        }
    }
}
