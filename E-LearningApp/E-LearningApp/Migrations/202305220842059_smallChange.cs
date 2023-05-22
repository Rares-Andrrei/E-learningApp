namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallChange : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StudentGradesAssociations", new[] { "Subjectid" });
            CreateIndex("dbo.StudentGradesAssociations", "SubjectId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StudentGradesAssociations", new[] { "SubjectId" });
            CreateIndex("dbo.StudentGradesAssociations", "Subjectid");
        }
    }
}
