using System.Security.Cryptography;
using System.Text;
using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class StudentManager
        : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentManager(IStudentRepository repository)
        {
            _repository = repository;
        }

        public Student Register(Student student, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            student.User.PasswordHash = passwordHash;
            student.User.PasswordSalt = passwordSalt;

            _repository.Add(student);

            return student;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public Student Login(string userName, string password)
        {
            var teacher = _repository.GetIncludedSingle(
                            filter: s => s.User.UserName == userName,
                            properties: s => s.User);

            if (teacher == null)
                return null;

            if (!VerifyPasswordHash(password, teacher.User.PasswordHash, teacher.User.PasswordSalt))
                return null;

            return teacher;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                        return false;
                }

                return true;
            }
        }

        public bool IsUserExist(string userName)
        {
            if (_repository.Get(t => t.User.UserName == userName) != null)
                return true;

            return false;
        }
    }
}