namespace HomeBudget.WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropCategory : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsExpense = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
