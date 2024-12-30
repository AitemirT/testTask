using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Repositories;

public class PhoneRepository : IPhoneRepository
{
    private readonly TestTaskContext _context;

    public PhoneRepository(TestTaskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Phone>> GetAllAsync()
    {
        return await _context.Phones.Include(p => p.User).ToListAsync();
    }

    public async Task<Phone?> GetByIdAsync(int? id)
    {
        return await _context.Phones.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Phone phone)
    {
        await _context.Phones.AddAsync(phone);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Phone phone)
    {
        _context.Phones.Update(phone);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(Phone phone)
    {
        _context.Phones.Remove(phone);
        await _context.SaveChangesAsync();
    }

    public bool Exists(int? id)
    {
        return _context.Phones.Any(p => p.Id == id);
    }
    
}