namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableSpecialization : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Classes", new[] { "SpecializationId" });
            AlterColumn("dbo.Classes", "SpecializationId", c => c.Int());
            CreateIndex("dbo.Classes", "SpecializationId");
            AddForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Classes", new[] { "SpecializationId" });
            AlterColumn("dbo.Classes", "SpecializationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "SpecializationId");
            AddForeignKey("dbo.Classes", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
        }
    }
}
