using ExamApp.Data;

using ExamApp.Entities;
using ExamApp.Extensions;
using ExamApp.Models;
using ExamApp.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ExamContext _examContext;   
        public UserRepository(ExamContext  examContext)
        {
            _examContext = examContext;
            if (!_examContext.User.Any())
            {
                _examContext.User.AddRange(new User[]
                    {
                             new User{ id=Guid.NewGuid().ToString(), Username ="admin".Encrypt(), Password ="123456A@!".Encrypt(), UserType=(int)UserType.Admin },
                             new User{ id=Guid.NewGuid().ToString(), Username ="user".Encrypt(), Password ="123456A@!".Encrypt(), UserType=(int)UserType.User }
                    });
                _examContext.SaveChanges();
            }
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await _examContext.User.AddAsync(user);
            return user;
     
        }

        public bool DeleteUser(string id)
        {
            var user =  _examContext.User.FirstOrDefault(x => x.id == id);
            var result = _examContext.User.Remove(user);
            _examContext.SaveChanges();
            return true;
        }

        public ResponseModel Login(User login)
        {
            ResponseModel response = new ResponseModel();
            var user = _examContext.User.FirstOrDefault(x => x.Username == login.Username.Encrypt() & x.Password == login.Password.Encrypt());
            if (user is null) { 
                response.Status = false;
                response.Error = "Kullanıcı Blunamadı";
                return response;
            }
            response.Response = user;
            response.Status = true;
            return response;
        }
    }
}
