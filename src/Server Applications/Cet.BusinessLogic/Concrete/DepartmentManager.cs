using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class DepartmentManager
        : GenericService<Department, IDepartmentRepository>, IDepartmentService
    {
        public DepartmentManager(IDepartmentRepository repository)
            : base(repository)
        {

        }
    }
}
