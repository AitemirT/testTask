using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TestTaskContext _context;

    public UserRepository(TestTaskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.Include(u => u.Phones).ToListAsync();
    }

    public async Task<User?> GetUserById(int? id)
    {
       return await _context.Users.Include(u => u.Phones).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Remove(User user)
    {
         _context.Users.Remove(user); 
         await _context.SaveChangesAsync();
    }
}