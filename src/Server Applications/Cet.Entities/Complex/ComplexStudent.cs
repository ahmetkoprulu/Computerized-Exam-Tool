using System;
using System.Collections.Generic;
using System.Text;

namespace Cet.Entities.Complex
{
    public class ComplexStudent
    {
        public ComplexStudent(int id, string name, string surname, string username, string email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            UserName = username;
            Questions = new List<ComplexQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<ComplexQuestion> Questions { get; set; }
    }
}
