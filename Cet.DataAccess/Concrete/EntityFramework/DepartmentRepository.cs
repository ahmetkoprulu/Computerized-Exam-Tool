using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class DepartmentRepository
        : Repository<Department, ApplicationDbContext>, IDepartmentRepository
    {
        
    }
}