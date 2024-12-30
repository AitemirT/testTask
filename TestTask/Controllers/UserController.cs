using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Repositories;
using TestTask.ViewModels;

namespace TestTask.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IPhoneRepository _phoneRepository;

    public UserController(IUserRepository userRepository, UserManager<User> userManager, IPhoneRepository phoneRepository)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _phoneRepository = phoneRepository;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<User> users = await _userRepository.GetUsers();
        return View(users);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        User user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        User user = await _userManager.FindByIdAsync(id.ToString());
        User currentUser = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        if (currentUser.Id != user.Id && !await _userManager.IsInRoleAsync(currentUser, "admin"))
        {
            return Forbid();
        }
        EditViewModel model = new EditViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName,
            DateOfBirth = user.DateOfBirth
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, EditViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            user.Email = model.Email;
            user.UserName = model.Username;
            user.DateOfBirth = model.DateOfBirth;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            return RedirectToAction("Index", "User");
        }
        return View(model);
    }
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        User user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        User user = await _userRepository.GetUserById(id);
        if (user != null)
        {
            await _userRepository.Remove(user);
        }
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
        }
        return View(model);
    }
}