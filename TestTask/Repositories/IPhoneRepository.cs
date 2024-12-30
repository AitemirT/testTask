using TestTask.Models;

namespace TestTask.Repositories;

public interface IPhoneRepository
{
    public Task<IEnumerable<Phone>> GetAllAsync();
    public Task<Phone?> GetByIdAsync(int? id);
    
    public Task AddAsync(Phone phone);
    public Task Update(Phone phone);
    public Task Remove(Phone phone);
    public bool Exists(int? id);
}