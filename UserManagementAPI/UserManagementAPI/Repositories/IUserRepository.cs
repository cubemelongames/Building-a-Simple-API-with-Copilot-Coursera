using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}