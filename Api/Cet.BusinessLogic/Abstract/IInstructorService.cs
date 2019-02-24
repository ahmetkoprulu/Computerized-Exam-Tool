using Cet.Core.BusinessLogic;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Abstract
{
    public interface IInstructorService
        : IRegistrableService<Instructor>, IService<Instructor>
    {
        
    }
}