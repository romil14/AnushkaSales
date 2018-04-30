namespace AnushkaSales.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerMobileNumberField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "MobileNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "MobileNumber", c => c.Int(nullable: false));
        }
    }
}
