using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class QuestionManager
        : GenericService<Question, IQuestionRepository>, IQuestionService
    {
        public QuestionManager(IQuestionRepository repository) : base(repository)
        {
        }
    }
}