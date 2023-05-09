namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thesisGrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGradesAssociations", "Thesis", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGradesAssociations", "Thesis");
        }
    }
}
