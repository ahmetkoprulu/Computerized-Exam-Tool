using System;
using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class Course : IEntity
    {
        public Course()
        {
            Exams = new HashSet<Exam>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsActive { get; set; }
        public int DepartmentId { get; set; }
        public int InstructorId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}