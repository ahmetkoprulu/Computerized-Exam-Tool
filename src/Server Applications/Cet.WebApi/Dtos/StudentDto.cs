using System.Collections.Generic;
using System.Security.AccessControl;
using Cet.Entities.Concrete;

namespace Cet.WebApi.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Role = "student";
        public string DepartmentName { get; set; }
        public string Token { get; set; }
        public ICollection<CourseDto> StudentCourses { get; set; }
    }
}