using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class ExamStatusManager
        : GenericService<ExamStatus, IExamStatusRepository>, IExamStatusService
    {
        public ExamStatusManager(IExamStatusRepository repository) 
            : base(repository)
        {

        }
    }
}