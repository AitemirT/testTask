using TestTask.Models;

namespace TestTask.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();
    public Task<User?> GetUserById(int? id);
    public Task Remove(User user);
}