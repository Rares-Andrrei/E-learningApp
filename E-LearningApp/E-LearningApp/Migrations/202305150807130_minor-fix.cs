namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubjectClassCategoryAssociations", "YearOfStudy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubjectClassCategoryAssociations", "YearOfStudy", c => c.Int(nullable: false));
        }
    }
}
