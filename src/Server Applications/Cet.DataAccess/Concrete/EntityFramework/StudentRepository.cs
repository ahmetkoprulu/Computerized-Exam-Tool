using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Complex;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class StudentRepository 
        : Repository<Student, ApplicationDbContext>, IStudentRepository
    {
        public Student GetStudentWithExams(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students
                    .Include(s => s.StudentCourseOfferings)
                    .ThenInclude(s => s.CourseOffering)
                    .ThenInclude(c => c.Exams)
                    .SingleOrDefault(s => s.Id == id);

                return student;
            }
        }

        public Student GetActiveExams(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students
                    .Include(s => s.StudentCourseOfferings)
                    .ThenInclude(s => s.CourseOffering)
                    //.ThenInclude(c => c.Exams.Where(e => e.Date.AddMinutes(-10) >= DateTime.Today))
                    .SingleOrDefault(s => s.Id == id);

                return student;
            }
        }

        public Student GetStudentForLogin(string userName)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students
                    .Include(s => s.User)
                    .Include(s => s.Department)
                    .Include(s => s.StudentCourseOfferings)
                    .ThenInclude(sco => sco.CourseOffering.Course)
                    .Include(s => s.StudentCourseOfferings)
                    .ThenInclude(sco => sco.CourseOffering.Exams)
                    .SingleOrDefault(s => s.User.UserName == userName);

                return student;
            }
        }

        public Student GetStudentWithCourses(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students
                    .Include(s => s.StudentCourseOfferings)
                    .ThenInclude(s => s.CourseOffering)
                    .ThenInclude(co => co.Course)
                    .SingleOrDefault(s => s.Id == id);

                return student;
            }
        }

        public void LogStudent(StudentActivities activity)
        {
            using(var context = new ApplicationDbContext())
            {
                context.StudentActivities.Add(activity);
                context.SaveChanges();
            }
        }

        public List<ComplexStudent> ListStudentsByExamId(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var questions = context.Questions.Where(q => q.ExamId == id).ToList();
                var students = (from s in context.Students
                                .Include(s => s.User)
                                .Include(s => s.Answers)
                             from a in context.Answers
                             from q in context.Questions
                             where q.ExamId == 30
                             where a.QuestionId == q.Id
                             where a.StudentId == s.Id
                             select s).Distinct().ToList<Student>();

                var students2 = new List<ComplexStudent>();

                foreach (var student in students)
                {
                    var tempStudent = new ComplexStudent(student.Id, student.User.Name,
                        student.User.Surname, student.User.Email, student.User.UserName);

                    foreach (var question in questions)
                    {
                        tempStudent.Questions.Add(new ComplexQuestion()
                        {
                            Text = question.Text,
                            Num = question.Num,
                            Extra = question.Extra,
                            AnswerText = student.Answers.FirstOrDefault(a => a.QuestionId == question.Id && a.StudentId == student.Id).Text
                        });
                    }

                    students2.Add(tempStudent);
                }
               
                return students2;
            }
        }

        public List<User> ListStudentsByCourseId(int id)
        {
            using(var context = new ApplicationDbContext())
            {
                var students = context.Users.FromSql(@"SELECT Users.Id, Users.Name, Users.Surname, Users.UserName, Users.Email, Users.PasswordHash, Users.PasswordSalt, PhotoId, PhotoUrl
                                                       FROM Users, Students, StudentCourseOfferings 
                                                       WHERE Users.Id = Students.Id 
                                                       AND Students.Id = StudentId 
                                                       AND CourseOfferingId={0}", id)
                                                       .ToList<User>();

                return students;
            }
        }
    }
}
