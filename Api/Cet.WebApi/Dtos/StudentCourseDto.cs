using System.Collections.Generic;

namespace Cet.WebApi.Dtos
{
    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<ExamDto> Exams { get; set; }
    }
}