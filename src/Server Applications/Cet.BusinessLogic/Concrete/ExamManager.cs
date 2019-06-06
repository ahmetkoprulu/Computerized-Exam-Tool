using System.Collections.Generic;
using System.Linq;
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

        public List<Exam> GetActiveExams(int courseId)
        {
            return Repository.GetActiveExams(courseId).OrderBy(e => e.Date).ToList();
        }

        public Exam AddExam(Exam entity)
        {
            return Repository.InsertExam(entity);
        }
    }
}