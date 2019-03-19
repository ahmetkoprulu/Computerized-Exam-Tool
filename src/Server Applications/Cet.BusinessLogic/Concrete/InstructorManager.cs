using System.Security.Cryptography;
using System.Text;
using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class InstructorManager
        : GenericService<Instructor, IInstructorRepository>, IInstructorService
    {
        private readonly IInstructorRepository _repository;

        public InstructorManager(IInstructorRepository repository) 
            : base(repository)
        {
            _repository = repository;
        }

        public Instructor Register(Instructor instructor, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            instructor.User.PasswordHash = passwordHash;
            instructor.User.PasswordSalt = passwordSalt;

            _repository.Add(instructor);

            return instructor;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public Instructor Login(string userName, string password)
        {
            var instructor = _repository.GetInstructorForLogin(userName);

            if (instructor == null)
                return null;

            if (!VerifyPasswordHash(password, instructor.User.PasswordHash, instructor.User.PasswordSalt))
                return null;

            return instructor;
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