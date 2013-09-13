namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingINdexToSortOnINPaymentVoucherEntries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentVoucherEntries", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentVoucherEntries", "Index");
        }
    }
}
