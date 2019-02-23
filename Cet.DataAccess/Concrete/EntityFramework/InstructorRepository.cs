using System;
using System.Collections.Generic;
using System.Text;
using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class InstructorRepository 
        : Repository<Instructor, ApplicationDbContext>, IInstructorRepository
    {
  
    }
}
