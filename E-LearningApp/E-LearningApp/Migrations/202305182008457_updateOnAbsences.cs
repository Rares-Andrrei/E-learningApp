namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOnAbsences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentAbsenceAssociations", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentAbsenceAssociations", "Semester");
        }
    }
}
