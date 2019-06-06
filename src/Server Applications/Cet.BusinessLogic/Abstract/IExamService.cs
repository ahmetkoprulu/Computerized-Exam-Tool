using Cet.Core.BusinessLogic;
using Cet.Entities.Concrete;
using System.Collections.Generic;

namespace Cet.BusinessLogic.Abstract
{
    public interface IExamService
        : IService<Exam>
    {
        List<Exam> GetActiveExams(int courseId);

        Exam AddExam(Exam entity);
    }
}