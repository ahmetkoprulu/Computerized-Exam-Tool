using Cet.Core.BusinessLogic;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Abstract
{
    public interface IStudentService
        : IRegistrableService<Student>, IService<Student>
    {
        Student GetStudentWithExams(int id);

        Student GetStudentWithCourses(int id);
    }
}