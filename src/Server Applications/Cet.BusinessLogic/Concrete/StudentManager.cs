using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Complex;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
    public class StudentManager
        : GenericService<Student, IStudentRepository>, IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentManager(IStudentRepository repository) 
            : base(repository)
        {
            _repository = repository;
        }

        public void RegisterStudentToCourse(StudentCourseOffering offering)
        {
            _repository.RegisterStudentToCourse(offering);
        }

        public Student GetStudentWithExams(int id)
        {
            return _repository.GetStudentWithExams(id);
        }

        public Student GetStudentActiveExams(int id)
        {
            return _repository.GetActiveExams(id);
        }

        public Student GetStudentWithCourses(int id)
        {
            return _repository.GetStudentWithCourses(id);
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
            var student = _repository.GetStudentForLogin(userName);

            if (student == null)
                return null;

            if (!VerifyPasswordHash(password, student.User.PasswordHash, student.User.PasswordSalt))
                return null;

            return student;
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

        public List<User> ListStudentsByCourseId(int id)
        {
            return _repository.ListStudentsByCourseId(id);
        }

        public List<ComplexStudent> ListStudentsByExamId(int id)
        {
            return _repository.ListStudentsByExamId(id);
        }

        public void LogStudent(StudentActivities activity)
        {
            _repository.LogStudent(activity);
        }
    }
}