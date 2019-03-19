using System.Collections.Generic;

namespace Cet.WebApi.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public List<ExamDto> Exams { get; set; }
    }
}