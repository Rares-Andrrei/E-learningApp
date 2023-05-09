namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseoptimization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Administrators", "UserId", "dbo.Users");
            DropForeignKey("dbo.Students", "ClassCategoryId", "dbo.ClassCategories");
            DropIndex("dbo.Administrators", new[] { "UserId" });
            DropIndex("dbo.Students", new[] { "ClassCategoryId" });
            DropColumn("dbo.Students", "ClassCategoryId");
            DropTable("dbo.Administrators");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Students", "ClassCategoryId", c => c.Int());
            CreateIndex("dbo.Students", "ClassCategoryId");
            CreateIndex("dbo.Administrators", "UserId");
            AddForeignKey("dbo.Students", "ClassCategoryId", "dbo.ClassCategories", "Id");
            AddForeignKey("dbo.Administrators", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
