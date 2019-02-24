using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class ExamStatusRepository
        : Repository<ExamStatus, ApplicationDbContext>, IExamStatusRepository
    {
        
    }
}