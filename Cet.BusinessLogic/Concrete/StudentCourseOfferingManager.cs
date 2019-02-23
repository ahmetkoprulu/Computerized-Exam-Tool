using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class StudentCourseOfferingManager
        : GenericService<StudentCourseOffering, IStudentCourseOfferingsRepository>, 
            IStudentCourseOfferingService
    {
        public StudentCourseOfferingManager(IStudentCourseOfferingsRepository repository) : base(repository)
        {

        }
    }
}