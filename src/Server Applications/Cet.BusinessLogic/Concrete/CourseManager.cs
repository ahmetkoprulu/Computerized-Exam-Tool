using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class CourseManager
        : GenericService<Course, ICourseRepository>, ICourseService
    {
        public CourseManager(ICourseRepository repository)
            : base(repository)
        {

        }
    }
}