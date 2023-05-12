namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseOptimization4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassCategories", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Classes", "ClassCategoryId", "dbo.ClassCategories");
            DropIndex("dbo.ClassCategories", new[] { "SpecializationId" });
            DropIndex("dbo.Classes", new[] { "ClassCategoryId" });
            AddColumn("dbo.Classes", "SpecializationId", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "YearOfStudy", c => c.String());
            CreateIndex("dbo.Classes", "SpecializationId");
            AddForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
            DropColumn("dbo.Classes", "ClassCategoryId");
            DropTable("dbo.ClassCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecializationId = c.Int(nullable: false),
                        YearOfStudy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Classes", "ClassCategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Classes", new[] { "SpecializationId" });
            DropColumn("dbo.Classes", "YearOfStudy");
            DropColumn("dbo.Classes", "SpecializationId");
            CreateIndex("dbo.Classes", "ClassCategoryId");
            CreateIndex("dbo.ClassCategories", "SpecializationId");
            AddForeignKey("dbo.Classes", "ClassCategoryId", "dbo.ClassCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassCategories", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
        }
    }
}
