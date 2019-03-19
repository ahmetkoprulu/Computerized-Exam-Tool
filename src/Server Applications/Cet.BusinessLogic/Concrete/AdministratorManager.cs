
using System.Security.Cryptography;
using System.Text;
using Cet.BusinessLogic.Abstract;
using Cet.Core.BusinessLogic;
using Cet.DataAccess.Abstract;
using Cet.Entities.Concrete;

namespace Cet.BusinessLogic.Concrete
{
     public class AdministratorManager
         : GenericService<Administrator, IAdministratorRepository>, IAdministratorService
     {
         private readonly IAdministratorRepository _repository;

         public AdministratorManager(IAdministratorRepository repository) 
             : base(repository)
         {
             _repository = repository;
         }

         public Administrator Register(Administrator admin, string password)
         {
             CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
             admin.User.PasswordHash = passwordHash;
             admin.User.PasswordSalt = passwordSalt;

             _repository.Add(admin);

             return admin;
         }

         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
         {
             using (var hmac = new HMACSHA512())
             {
                 passwordSalt = hmac.Key;
                 passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
             }
         }

         public Administrator Login(string userName, string password)
         {
             var admin = _repository.GetIncludedSingle(
                            filter: a => a.User.UserName == userName,
                            properties: a => a.User);

             if (admin == null)
                 return null;

             if (!VerifyPasswordHash(password, admin.User.PasswordHash, admin.User.PasswordSalt))
                 return null;

             return admin;
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
