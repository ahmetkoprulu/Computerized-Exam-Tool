using System;
using System.Collections.Generic;

namespace Cet.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }
    }
}