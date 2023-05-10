namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseOptimization2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassCategories", "YearofStudyId", "dbo.YearOfStudies");
            DropIndex("dbo.ClassCategories", new[] { "YearofStudyId" });
            AddColumn("dbo.ClassCategories", "YearOfStudy", c => c.Int(nullable: false));
            DropColumn("dbo.ClassCategories", "YearofStudyId");
            DropTable("dbo.YearOfStudies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.YearOfStudies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ClassCategories", "YearofStudyId", c => c.Int(nullable: false));
            DropColumn("dbo.ClassCategories", "YearOfStudy");
            CreateIndex("dbo.ClassCategories", "YearofStudyId");
            AddForeignKey("dbo.ClassCategories", "YearofStudyId", "dbo.YearOfStudies", "Id", cascadeDelete: true);
        }
    }
}
