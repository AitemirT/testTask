using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Models;

public class TestTaskContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Phone> Phones { get; set; }
    public TestTaskContext(DbContextOptions<TestTaskContext> options) : base(options){}
}