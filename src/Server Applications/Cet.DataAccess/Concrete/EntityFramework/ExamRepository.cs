using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class ExamRepository
        : Repository<Exam, ApplicationDbContext>, IExamRepository
    {
        public List<Exam> GetActiveExams(int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var now = System.DateTime.Now.ToString("yyyy-MM-dd");
                return context.Exams.FromSql("SELECT * FROM Exams WHERE Exams.Date > {0}", now)
                    .ToList();
            }
        }

        public Exam InsertExam(Exam entity)
        {
            using (var context = new ApplicationDbContext())
            {
                var query = ("INSERT INTO Exams(Name, CourseOfferingId, Date, Duration, ExamStatusId) VALUES('{0}', {1}, '{2}', {3}, {4})", entity.Name, entity.CourseOfferingId, entity.Date, entity.Duration, 1);
                context.Database.ExecuteSqlCommand("INSERT INTO Exams(Name, CourseOfferingId, Date, Duration, ExamStatusId) VALUES({0}, {1}, {2}, {3}, {4})", entity.Name, entity.CourseOfferingId, entity.Date.ToString("yyyy-MM-dd HH:mm:ss"), entity.Duration, 1);
                context.SaveChanges();
                return context.Exams.FromSql("SELECT * FROM Exams WHERE Exams.Name = {0} AND Exams.CourseOfferingId={1} AND Exams.Date = {2} AND Exams.Duration = {3}", entity.Name, entity.CourseOfferingId, entity.Date.ToString("yyyy-MM-dd HH:mm:ss"), entity.Duration)
                    .FirstOrDefault();
            }
        }
    }
}