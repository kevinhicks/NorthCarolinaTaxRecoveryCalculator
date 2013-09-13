namespace NorthCarolinaTaxRecoveryCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentVoucherEntries", "ID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentVoucherEntries", "ID", c => c.Int(nullable: false, identity: true));
        }
    }
}
