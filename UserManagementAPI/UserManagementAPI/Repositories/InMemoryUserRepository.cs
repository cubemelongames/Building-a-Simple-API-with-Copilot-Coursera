using System.Collections.Concurrent;
using System.Threading;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private int _idSeq = 0;

        public InMemoryUserRepository()
        {
            // seed 1 user so GET shows something
            Create(new User { Name = "Admin User", Email = "admin@techhive.local", Department = "IT", Title = "Admin" });
        }

        public IEnumerable<User> GetAll()
            => _users.Values.OrderBy(u => u.Id);

        public User? GetById(int id)
            => _users.TryGetValue(id, out var u) ? u : null;

        public User Create(User user)
        {
            var id = Interlocked.Increment(ref _idSeq);
            var created = new User
            {
                Id = id,
                Name = user.Name.Trim(),
                Email = user.Email.Trim(),
                Department = string.IsNullOrWhiteSpace(user.Department) ? null : user.Department.Trim(),
                Title = string.IsNullOrWhiteSpace(user.Title) ? null : user.Title.Trim()
            };

            _users[id] = created;
            return created;
        }

        public bool Update(int id, User user)
        {
            if (!_users.ContainsKey(id)) return false;

            var updated = new User
            {
                Id = id,
                Name = user.Name.Trim(),
                Email = user.Email.Trim(),
                Department = string.IsNullOrWhiteSpace(user.Department) ? null : user.Department.Trim(),
                Title = string.IsNullOrWhiteSpace(user.Title) ? null : user.Title.Trim()
            };

            _users[id] = updated;
            return true;
        }

        public bool Delete(int id)
            => _users.TryRemove(id, out _);
    }
}