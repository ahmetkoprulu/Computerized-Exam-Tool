using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class QuestionTypeManager
        : GenericService<QuestionType, IQuestionTypeRepository>, IQuestionTypeService
    {
        public QuestionTypeManager(IQuestionTypeRepository repository) : base(repository)
        {

        }
    }
}