using Cet.Core.DataAccess;
using Cet.Entities.Concrete;
using System.Collections.Generic;

namespace Cet.DataAccess.Abstract
{
    public interface IExamRepository
        : IRepository<Exam>

    {
        List<Exam> GetActiveExams(int courseId);

        Exam InsertExam(Exam entity);
    }
}