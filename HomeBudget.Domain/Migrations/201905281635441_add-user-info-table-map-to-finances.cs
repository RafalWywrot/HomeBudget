namespace HomeBudget.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserinfotablemaptofinances : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categories", newName: "Category");
            RenameTable(name: "dbo.Finances", newName: "Finance");
            CreateTable(
                "dbo.UserInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(),
                    LastName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.AspNetUsers", "UserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfo", "Id");
            AddColumn("dbo.Finance", "UserInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Finance", "UserInfoId");
            AddForeignKey("dbo.Finance", "UserInfoId", "dbo.UserInfo", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserInfo_Id", "dbo.UserInfo");
            DropIndex("dbo.AspNetUsers", new[] { "UserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "UserInfo_Id");
            DropTable("dbo.UserInfo");
            RenameTable(name: "dbo.Finance", newName: "Finances");
            RenameTable(name: "dbo.Category", newName: "Categories");
            DropForeignKey("dbo.Finance", "UserInfoId", "dbo.UserInfo");
            DropIndex("dbo.Finance", new[] { "UserInfoId" });
            DropColumn("dbo.Finance", "UserInfoId");
        }
    }
}
