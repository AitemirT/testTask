using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.ViewModels;

public class RegisterViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Имя пользователя не может быть пустым")]
    [Remote(action: "CheckUserName", controller: "Validation", ErrorMessage = "Пользователь с таким именем пользователя уже существует")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Почта пользователя обязательна к заполнению")]
    [EmailAddress]
    [Remote(action: "CheckUserEmail", controller: "Validation", ErrorMessage = "Пользователь с такой почтой уже существует", AdditionalFields = "Id")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Пароль не может быть пустым")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [Remote(action:"CheckBirthDate", controller:"Validation", ErrorMessage = "Возраст не может быть больше 100 и находиться в будущем")]
    public DateOnly DateOfBirth { get; set; }
}