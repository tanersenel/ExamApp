using ExamApp.Entities;
using ExamApp.Models;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        bool DeleteUser(string id);
        ResponseModel Login(User user);


    }
}
