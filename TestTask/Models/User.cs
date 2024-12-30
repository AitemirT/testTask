using Microsoft.AspNetCore.Identity;

namespace TestTask.Models;

public class User : IdentityUser<int>
{
    public DateOnly DateOfBirth { get; set; }
    public List<Phone>  Phones { get; set; } = new List<Phone>();
    
}