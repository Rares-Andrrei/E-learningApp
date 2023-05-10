namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseOptimization3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassCategoryThesisAssociations", "ClassCategoryId", "dbo.ClassCategories");
            DropForeignKey("dbo.ClassCategoryThesisAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectClassAssociations", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.SubjectClassAssociations", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.ClassCategoryThesisAssociations", new[] { "ClassCategoryId" });
            DropIndex("dbo.ClassCategoryThesisAssociations", new[] { "SubjectId" });
            DropIndex("dbo.SubjectClassAssociations", new[] { "SubjectId" });
            DropIndex("dbo.SubjectClassAssociations", new[] { "ClassId" });
            CreateTable(
                "dbo.SubjectClassCategoryAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Thesis = c.Boolean(nullable: false),
                        ClassCategortId = c.Int(nullable: false),
                        ClassCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassCategory_Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.ClassCategory_Id);
            
            DropTable("dbo.ClassCategoryThesisAssociations");
            DropTable("dbo.SubjectClassAssociations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubjectClassAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassCategoryThesisAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassCategoryId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.SubjectClassCategoryAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id", "dbo.Classes");
            DropIndex("dbo.SubjectClassCategoryAssociations", new[] { "ClassCategory_Id" });
            DropIndex("dbo.SubjectClassCategoryAssociations", new[] { "SubjectId" });
            DropTable("dbo.SubjectClassCategoryAssociations");
            CreateIndex("dbo.SubjectClassAssociations", "ClassId");
            CreateIndex("dbo.SubjectClassAssociations", "SubjectId");
            CreateIndex("dbo.ClassCategoryThesisAssociations", "SubjectId");
            CreateIndex("dbo.ClassCategoryThesisAssociations", "ClassCategoryId");
            AddForeignKey("dbo.SubjectClassAssociations", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubjectClassAssociations", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassCategoryThesisAssociations", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassCategoryThesisAssociations", "ClassCategoryId", "dbo.ClassCategories", "Id", cascadeDelete: true);
        }
    }
}
