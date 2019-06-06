using System.Collections.Generic;
using Cet.Core.BusinessLogic;
using Cet.Entities.Complex;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Abstract
{
    public interface IStudentService
        : IRegistrableService<Student>, IService<Student>
    {
        Student GetStudentWithExams(int id);

        Student GetStudentActiveExams(int id);

        Student GetStudentWithCourses(int id);

        List<User> ListStudentsByCourseId(int id);

        List<ComplexStudent> ListStudentsByExamId(int id);

        void LogStudent(StudentActivities activity);
    }
}