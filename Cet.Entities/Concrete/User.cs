using Cet.Core.Entities;

namespace Cet.Entities.Concrete
{
    public partial class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual Administrator Administrator { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Student Student { get; set; }
    }
}