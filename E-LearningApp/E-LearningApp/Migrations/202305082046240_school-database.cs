namespace E_LearningApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schooldatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecializationId = c.Int(nullable: false),
                        YearofStudyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .ForeignKey("dbo.YearOfStudies", t => t.YearofStudyId, cascadeDelete: true)
                .Index(t => t.SpecializationId)
                .Index(t => t.YearofStudyId);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YearOfStudies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassCategories", t => t.ClassCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.ClassCategoryId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClassCategoryId = c.Int(nullable: false),
                        ClassMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassCategories", t => t.ClassCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Professors", t => t.ClassMasterId)
                .Index(t => t.ClassCategoryId)
                .Index(t => t.ClassMasterId);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonalDataId, cascadeDelete: true)
                .Index(t => t.PersonalDataId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfessorSubjectAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfessorId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professors", t => t.ProfessorId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.ProfessorId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.StudentAbsenceAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        AbsenceDate = c.DateTime(nullable: false),
                        Reasoned = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalDataId = c.Int(nullable: false),
                        ClassCategoryId = c.Int(),
                        ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.ClassCategories", t => t.ClassCategoryId)
                .ForeignKey("dbo.People", t => t.PersonalDataId, cascadeDelete: true)
                .Index(t => t.PersonalDataId)
                .Index(t => t.ClassCategoryId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.StudentGradesAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        Semester = c.Int(nullable: false),
                        Subjectid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subjectid, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.Subjectid);
            
            CreateTable(
                "dbo.SubjectClassAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectClassAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectClassAssociations", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.StudentGradesAssociations", "Subjectid", "dbo.Subjects");
            DropForeignKey("dbo.StudentGradesAssociations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentAbsenceAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentAbsenceAssociations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "PersonalDataId", "dbo.People");
            DropForeignKey("dbo.Students", "ClassCategoryId", "dbo.ClassCategories");
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.ProfessorSubjectAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ProfessorSubjectAssociations", "ProfessorId", "dbo.Professors");
            DropForeignKey("dbo.Classes", "ClassMasterId", "dbo.Professors");
            DropForeignKey("dbo.Professors", "PersonalDataId", "dbo.People");
            DropForeignKey("dbo.Classes", "ClassCategoryId", "dbo.ClassCategories");
            DropForeignKey("dbo.ClassCategoryThesisAssociations", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ClassCategoryThesisAssociations", "ClassCategoryId", "dbo.ClassCategories");
            DropForeignKey("dbo.ClassCategories", "YearofStudyId", "dbo.YearOfStudies");
            DropForeignKey("dbo.ClassCategories", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.SubjectClassAssociations", new[] { "ClassId" });
            DropIndex("dbo.SubjectClassAssociations", new[] { "SubjectId" });
            DropIndex("dbo.StudentGradesAssociations", new[] { "Subjectid" });
            DropIndex("dbo.StudentGradesAssociations", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropIndex("dbo.Students", new[] { "ClassCategoryId" });
            DropIndex("dbo.Students", new[] { "PersonalDataId" });
            DropIndex("dbo.StudentAbsenceAssociations", new[] { "SubjectId" });
            DropIndex("dbo.StudentAbsenceAssociations", new[] { "StudentId" });
            DropIndex("dbo.ProfessorSubjectAssociations", new[] { "SubjectId" });
            DropIndex("dbo.ProfessorSubjectAssociations", new[] { "ProfessorId" });
            DropIndex("dbo.Professors", new[] { "PersonalDataId" });
            DropIndex("dbo.Classes", new[] { "ClassMasterId" });
            DropIndex("dbo.Classes", new[] { "ClassCategoryId" });
            DropIndex("dbo.ClassCategoryThesisAssociations", new[] { "SubjectId" });
            DropIndex("dbo.ClassCategoryThesisAssociations", new[] { "ClassCategoryId" });
            DropIndex("dbo.ClassCategories", new[] { "YearofStudyId" });
            DropIndex("dbo.ClassCategories", new[] { "SpecializationId" });
            DropTable("dbo.SubjectClassAssociations");
            DropTable("dbo.StudentGradesAssociations");
            DropTable("dbo.Students");
            DropTable("dbo.StudentAbsenceAssociations");
            DropTable("dbo.ProfessorSubjectAssociations");
            DropTable("dbo.People");
            DropTable("dbo.Professors");
            DropTable("dbo.Classes");
            DropTable("dbo.Subjects");
            DropTable("dbo.ClassCategoryThesisAssociations");
            DropTable("dbo.YearOfStudies");
            DropTable("dbo.Specializations");
            DropTable("dbo.ClassCategories");
        }
    }
}
