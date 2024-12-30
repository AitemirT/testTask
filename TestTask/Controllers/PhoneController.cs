using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;
using TestTask.Repositories;

namespace TestTask.Controllers
{
    [Authorize]
    public class PhoneController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IUserRepository _userRepository;

        public PhoneController(IPhoneRepository phoneRepository, IUserRepository userRepository)
        {
            _phoneRepository = phoneRepository;
            _userRepository = userRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Phone> phones = await _phoneRepository.GetAllAsync();
            return View(phones);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Phone? phone = await _phoneRepository.GetByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userRepository.GetUsers();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                await _phoneRepository.AddAsync(phone);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = await _userRepository.GetUsers();
            return View(phone);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _phoneRepository.GetByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewBag.Users = await _userRepository.GetUsers();
            return View(phone);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _phoneRepository.Update(phone);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = await _userRepository.GetUsers();
            return View(phone);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Phone phone = await _phoneRepository.GetByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }
        
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Phone? phone = await _phoneRepository.GetByIdAsync(id);
            if (phone != null)
            {
                _phoneRepository.Remove(phone);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
            return _phoneRepository.Exists(id);
        }
    }
}
