using System;
using System.Collections.Generic;

namespace Cet.Entities
{
    public partial class CourseOffering
    {
        public int Id { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public bool? IsActive { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
    }
}