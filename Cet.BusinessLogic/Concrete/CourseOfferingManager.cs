using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class CourseOfferingManager
        : GenericService<CourseOffering, ICourseOfferingRepository>, ICourseOfferingService
    {
        public CourseOfferingManager(ICourseOfferingRepository repository) 
            : base(repository)
        {

        }
    }
}