using ExamApp.Entities;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        bool DeleteUser(string id);
        User Login(User user);


    }
}
