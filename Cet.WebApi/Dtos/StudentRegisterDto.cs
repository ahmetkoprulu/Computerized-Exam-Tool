namespace Cet.WebApi.Dtos
{
    public class StudentRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }
}