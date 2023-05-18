namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGradeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGradesAssociations", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGradesAssociations", "Date");
        }
    }
}
