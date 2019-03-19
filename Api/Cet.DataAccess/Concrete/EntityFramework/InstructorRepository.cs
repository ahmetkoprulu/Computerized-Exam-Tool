using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cet.Core.DataAccess.EntityFramework;
using Cet.DataAccess.Abstract;
using Cet.DataAccess.Concrete.EntityFramework;
using Cet.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Cet.DataAccess.Concrete.EntityFramework
{
    public class InstructorRepository 
        : Repository<Instructor, ApplicationDbContext>, IInstructorRepository
    {
        public Instructor GetInstructorForLogin(string userName)
        {
            using (var context = new ApplicationDbContext())
            {
                var instructor = context.Instructors
                    .Include(s => s.User)
                    .Include(s => s.Department)
                    .Include(s => s.CourseOfferings)
                    .ThenInclude(sco => sco.Course)
                    .Include(s => s.CourseOfferings)
                    .ThenInclude(sco => sco.Exams)
                    .SingleOrDefault(s => s.User.UserName == userName);

                return instructor;
            }
        }
    }
}
