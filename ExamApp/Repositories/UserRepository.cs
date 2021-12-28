using ExamApp.Data;

using ExamApp.Entities;
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
                             new User{ id=Guid.NewGuid().ToString(), Username ="admin", Password ="123456A@!" }
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

        public  User Login(User login)
        {
            var user = _examContext.User.FirstOrDefault(x => x.Username == login.Username & x.Password == login.Password);
            _ = user ?? throw new ArgumentNullException(nameof(user));
            return user;
        }
    }
}
