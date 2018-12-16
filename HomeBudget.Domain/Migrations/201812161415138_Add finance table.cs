namespace HomeBudget.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addfinancetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Finances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Finances", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Finances", new[] { "CategoryId" });
            DropTable("dbo.Finances");
        }
    }
}
