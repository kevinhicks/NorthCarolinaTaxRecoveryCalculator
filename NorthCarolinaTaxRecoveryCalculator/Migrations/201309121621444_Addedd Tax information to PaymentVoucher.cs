namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeddTaxinformationtoPaymentVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentVouchers", "TaxCostElement", c => c.String(nullable: false));
            AddColumn("dbo.PaymentVouchers", "TaxAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentVouchers", "TaxAmount");
            DropColumn("dbo.PaymentVouchers", "TaxCostElement");
        }
    }
}
