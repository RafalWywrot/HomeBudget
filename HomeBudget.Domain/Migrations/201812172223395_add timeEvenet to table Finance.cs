namespace HomeBudget.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimeEvenettotableFinance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Finances", "TimeEvent", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Finances", "TimeEvent");
        }
    }
}
