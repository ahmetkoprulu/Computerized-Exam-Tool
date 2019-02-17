using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class AnswerManager
        : GenericService<Answer, IAnswerRepository>, IAnswerService
    {
        public AnswerManager(IAnswerRepository repository) : base(repository)
        {

        }
    }
}