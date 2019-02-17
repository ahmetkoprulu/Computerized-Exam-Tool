using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class ExamManager
        : GenericService<Exam, IExamRepository>, IExamService
    {
        public ExamManager(IExamRepository repository)
            : base(repository)
        {

        }
    }
}