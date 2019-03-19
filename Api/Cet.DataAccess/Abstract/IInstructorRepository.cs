using System;
using System.Collections.Generic;
using System.Text;
using Cet.Core.DataAccess;
using Cet.Core.Entities;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Abstract
{
    public interface IInstructorRepository 
        : IRepository<Instructor>
    {
        Instructor GetInstructorForLogin(string userName);
    }
}
