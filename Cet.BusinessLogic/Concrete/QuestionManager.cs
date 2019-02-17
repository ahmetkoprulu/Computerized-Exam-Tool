using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class QuestionManager
        : GenericService<Question, QuestionRepository>, IQuestionService
    {
        public QuestionManager(QuestionRepository repository) : base(repository)
        {
        }
    }
}