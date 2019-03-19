using System.Collections.Generic;

namespace Cet.WebApi.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public string DepartmentName { get; set; }
        public string Token { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}