using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
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
    }
}
