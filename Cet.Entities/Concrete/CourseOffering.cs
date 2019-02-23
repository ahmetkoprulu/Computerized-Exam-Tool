using System;
using System.Collections.Generic;
using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class CourseOffering : IEntity
    {
        public CourseOffering()
        {
            Exams = new HashSet<Exam>();
            StudentCourseOfferings = new HashSet<StudentCourseOffering>();
        }

        public int Id { get; set; }
        public string Semester { get; set; }
        public DateTime Year { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<StudentCourseOffering> StudentCourseOfferings { get; set; }
    }
}