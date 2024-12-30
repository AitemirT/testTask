using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;

namespace TestTask.Controllers;

public class ValidationController : Controller
{
    private readonly UserManager<User> _userManager;

    public ValidationController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    [AcceptVerbs("GET", "POST")]
    public IActionResult CheckBirthDate(DateTime dateOfBirth)
    {
        DateTime today = DateTime.Now;
        DateTime minDate = today.AddYears(-100);
        if (dateOfBirth > today)
        {
            return Json(false);
        }

        if (dateOfBirth < minDate)
        {
            return Json(false);
        }

        return Json(true);
    }
    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> CheckUserEmail(string email, int id)
    {
        User user = await _userManager.FindByEmailAsync(email);

        if (user != null && user.Id != id)
        {
            return Json(false);
        }
        return Json(true);
    }
    
    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> CheckUserName(string username, int id)
    {
        User user = await _userManager.FindByNameAsync(username);

        if (user != null && user.Id != id)
        {
            return Json(false);
        }

        return Json(true);
    }
}