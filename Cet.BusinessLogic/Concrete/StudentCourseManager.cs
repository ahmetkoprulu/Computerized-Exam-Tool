using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class StudentCourseManager
        : GenericService<StudentCourse, IStudentCourseRepository>, IStudentCourseService
    {
        public StudentCourseManager(IStudentCourseRepository repository) : 
            base(repository)
        {
        }
    }
}