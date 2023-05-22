using Azure;
using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.BusinessLogicLayer.ProfessorF
{
    public class ProfessorBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public ProfessorBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public string GetProfessorEmailByUserId(int userId)
        {
            return UnitOfWork.Users.GetEmailByUserId(userId);
        }
        public int GetProfessorIdByUserId(int userId)
        {
           return  UnitOfWork.Professors.GetProfessorIdByUserId(userId);
        }
        public List<ProfessorClassGradesDto> GetProfessorsSubjectsAndGrades(int professorId)
        {
            List<ProfessorClassGradesDto> list = new List<ProfessorClassGradesDto>();

            var grades  = UnitOfWork.ProfessorSubjectAssociationDL.GetAllByProfessor(professorId);
            foreach (var item in grades)
            {
                ProfessorClassGradesDto exists = list.Where(r => r.ClassId == item.ClassId && r.SubjectId == item.SubjectId).FirstOrDefault();
                if (exists == null)
                {
                    exists = new ProfessorClassGradesDto { SubjectId = item.SubjectId, ClassId = item.ClassId , ClassName = item.Class.Name, SubjectName = item.Subject.Name};
                    list.Add(exists);
                }
                List<EntityFullNameIdDto> students = UnitOfWork.StudentsDL.GetStudentsFromClass(item.ClassId);
                foreach (var student in students)
                {
                    StudentSubjectGradesDto2 studentGrades = new StudentSubjectGradesDto2 { StudentName = student.FullName, StudentId = student.Id };

                    List<StudentGradesAssociation> studentsGrades = UnitOfWork.StudentSubjectGradesDL.GetStudentGradesForSubject(student.Id, item.SubjectId);
                    foreach(var grade in studentsGrades)
                    {
                        if (grade.Semester == 1)
                        {
                            studentGrades.FirstSemesterGrades.Add(new GradeDto { Date = grade.Date, Grade = grade.Grade, IsThesis = grade.Thesis, Id = grade.Id});
                        }
                        else
                        {
                            studentGrades.SecondSemesterGrades.Add(new GradeDto { Date = grade.Date, Grade = grade.Grade, IsThesis = grade.Thesis, Id = grade.Id });
                        }
                    }
                    exists.Grades.Add(studentGrades);
                }

            }
            return list;
        }

        public List<StudentFullNameClassDto> GetStudents(int professorId)
        {
            var classes = UnitOfWork.ProfessorSubjectAssociationDL.GetAllIdsByProfessor(professorId);
            List<StudentFullNameClassDto> list = new List<StudentFullNameClassDto>();
            foreach (var @class in classes)
            {
                list.AddRange(UnitOfWork.StudentsDL.GetStudentsFromClass2(@class));
            }
            return list;
        }
        public List<SubjectHasThesisDto> GetSubjectOptionsForStudent(int professorId, int classId) 
        {
            var subjects = UnitOfWork.ProfessorSubjectAssociationDL.GetSubjectsProfessorClass(professorId, classId);
            List<SubjectHasThesisDto> list = new List<SubjectHasThesisDto>();
            Class @class = UnitOfWork.Classes.GetById(classId);
            foreach (var subject in subjects)
            {
                bool hasThesis = UnitOfWork.SubjectClassCategoryAssociationDL.ClassSubjectIsThesis(subject.Id, @class.YearOfStudy, @class.SpecializationId);
                list.Add(new SubjectHasThesisDto { HasThesis = hasThesis, Subject = subject });
            }
            return list;
        }
        public void AddStudentGrade(StudentGradesAssociation studentGradesAssociation)
        {
            if (studentGradesAssociation.Date.Year <= DateTime.Now.Year - 200)
            {
                return;
            }
            UnitOfWork.StudentSubjectGradesDL.Insert(studentGradesAssociation);
            UnitOfWork.SaveChanges();
        }
        public void DeleteGrade(int gradeId)
        {
            var record = UnitOfWork.StudentSubjectGradesDL.GetById(gradeId);

            if (record != null)
            {
                UnitOfWork.StudentSubjectGradesDL.Remove(record);
                UnitOfWork.SaveChanges();
            }
        }
        public List<ClassDto> GetClassesForProfessor(int professorId)
        {
            return UnitOfWork.ProfessorSubjectAssociationDL.GetClassesForProfessor(professorId).Select(r=>new ClassDto { Name =r.Name, Id = r.Id}).ToList();
        }

        public List<Subject> SubjectsProfessorClass(int professorId, int classId)
        {
            return UnitOfWork.ProfessorSubjectAssociationDL.GetAllAndSubejcts().Where(r => r.ClassId == classId && r.ProfessorId == professorId).Select(r => r.Subject).ToList();
        }
        public List<StudentAbsencesDto> GetStudentAbsences(int classId, int subjectId)
        {
            var students = UnitOfWork.StudentsDL.GetAllWithPersonalData().Where(r => r.ClassId == classId);
            List <StudentAbsencesDto> absencesList = new List<StudentAbsencesDto>();
            foreach (var student in students)
            { 
                StudentAbsencesDto item = new StudentAbsencesDto { StudentId = student.Id, StudentName = student.PersonalData.FirstName + " " + student.PersonalData.LastName };
                absencesList.Add(item);
                var studentAbsenceList = UnitOfWork.StudentAbsenceAssociationDL.GetStudentsAbsences(student.Id).Where(r=>r.SubjectId == subjectId);
                foreach (var studentAbsence in studentAbsenceList)
                {
                    if (studentAbsence.Semester == 1)
                    {
                        item.FirstSemesterAbsences.Add(new AbsenceInfoDto { Id = studentAbsence.Id, Date = studentAbsence.AbsenceDate, Reasoned = studentAbsence.Reasoned });
                    }
                    else
                    {
                        item.SecondSemesterAbsences.Add(new AbsenceInfoDto { Id = studentAbsence.Id, Date = studentAbsence.AbsenceDate, Reasoned = studentAbsence.Reasoned });
                    }
                }
            }
            return absencesList;
        }
        public List<EntityFullNameIdDto> GetStudentsFromClass(int classId)
        {
            return UnitOfWork.StudentsDL.GetStudentsFromClass(classId);
        }
        public void AddAbsence(StudentAbsenceAssociation studentAbsenceAssociation)
        {
            UnitOfWork.StudentAbsenceAssociationDL.Insert(studentAbsenceAssociation);
            UnitOfWork.SaveChanges();
        }
        public void MotivateAbsence(int absenceId)
        {
            var absence = UnitOfWork.StudentAbsenceAssociationDL.GetById(absenceId);
            absence.Reasoned = true;
            UnitOfWork.StudentAbsenceAssociationDL.Update(absence);
            UnitOfWork.SaveChanges();
        }
        public void DeleteAbsence(int absenceId)
        {
            var absence = UnitOfWork.StudentAbsenceAssociationDL.GetById(absenceId);
            UnitOfWork.StudentAbsenceAssociationDL.Remove(absence);
            UnitOfWork.SaveChanges();
        }
    }
}
