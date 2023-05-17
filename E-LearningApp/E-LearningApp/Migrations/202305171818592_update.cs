namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Classes", new[] { "SpecializationId" });
            AddColumn("dbo.ProfessorSubjectAssociations", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Classes", "SpecializationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "SpecializationId");
            CreateIndex("dbo.ProfessorSubjectAssociations", "ClassId");
            AddForeignKey("dbo.ProfessorSubjectAssociations", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.ProfessorSubjectAssociations", "ClassId", "dbo.Classes");
            DropIndex("dbo.ProfessorSubjectAssociations", new[] { "ClassId" });
            DropIndex("dbo.Classes", new[] { "SpecializationId" });
            AlterColumn("dbo.Classes", "SpecializationId", c => c.Int());
            DropColumn("dbo.ProfessorSubjectAssociations", "ClassId");
            CreateIndex("dbo.Classes", "SpecializationId");
            AddForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations", "Id");
        }
    }
}
