﻿using System;
using System.Collections.Generic;
using System.Text;
using Cet.Core.DataAccess;
using Cet.Entities.Concrete;

namespace Cet.DataAccess.Abstract
{
    public interface IStudentRepository 
        :IRepository<Student>
    {
        Student GetStudentWithExams(int id);

        Student GetStudentWithCourses(int id);
    }
}
