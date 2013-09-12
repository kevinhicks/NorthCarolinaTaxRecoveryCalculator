namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePaymentVoucherEntry_IDintoanint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentVoucherEntries", "ID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentVoucherEntries", "ID", c => c.Guid(nullable: false));
        }
    }
}
