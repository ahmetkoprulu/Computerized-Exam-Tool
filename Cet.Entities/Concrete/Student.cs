using System;
using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Student : IRegistrable
    {
        public Student()
        {
            Answers = new HashSet<Answer>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}