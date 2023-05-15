namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id", "dbo.Classes");
            DropIndex("dbo.SubjectClassCategoryAssociations", new[] { "ClassCategory_Id" });
            AddColumn("dbo.SubjectClassCategoryAssociations", "YearOfStudy", c => c.Int(nullable: false));
            AddColumn("dbo.SubjectClassCategoryAssociations", "SpecializationId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubjectClassCategoryAssociations", "SpecializationId");
            AddForeignKey("dbo.SubjectClassCategoryAssociations", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
            DropColumn("dbo.SubjectClassCategoryAssociations", "ClassCategortId");
            DropColumn("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id", c => c.Int());
            AddColumn("dbo.SubjectClassCategoryAssociations", "ClassCategortId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SubjectClassCategoryAssociations", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.SubjectClassCategoryAssociations", new[] { "SpecializationId" });
            DropColumn("dbo.SubjectClassCategoryAssociations", "SpecializationId");
            DropColumn("dbo.SubjectClassCategoryAssociations", "YearOfStudy");
            CreateIndex("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id");
            AddForeignKey("dbo.SubjectClassCategoryAssociations", "ClassCategory_Id", "dbo.Classes", "Id");
        }
    }
}
