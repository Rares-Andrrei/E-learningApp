namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Professors", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Professors", "UserId");
            CreateIndex("dbo.Students", "UserId");
            AddForeignKey("dbo.Professors", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Students", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "UserId", "dbo.Users");
            DropForeignKey("dbo.Professors", "UserId", "dbo.Users");
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.Professors", new[] { "UserId" });
            DropColumn("dbo.Students", "UserId");
            DropColumn("dbo.Professors", "UserId");
        }
    }
}
