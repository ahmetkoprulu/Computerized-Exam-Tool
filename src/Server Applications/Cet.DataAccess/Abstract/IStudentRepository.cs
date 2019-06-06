using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Cet.Core.DataAccess;
using Cet.Entities.Complex;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Abstract
{
    public interface IStudentRepository 
        :IRepository<Student>
    {
        Student GetStudentWithExams(int id);

        Student GetActiveExams(int id);

        Student GetStudentWithCourses(int id);

        Student GetStudentForLogin(string userName);

        List<User> ListStudentsByCourseId(int id);

        List<ComplexStudent> ListStudentsByExamId(int id);

        void LogStudent(StudentActivities activity);
    }
}
