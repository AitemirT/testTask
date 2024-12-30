using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.ViewModels;

public class EditViewModel
{
    public int Id {get; set;}
    [Required(ErrorMessage = "Имя не может быть пустым")]
    [Remote(action: "CheckUserName", controller:"Validation", ErrorMessage = "Пользователь с таким именем уже есть", AdditionalFields = "Username,Id")]
    public string Username {get; set;}
    [Required]
    [Remote(action: "CheckUserEmail", controller: "Validation", ErrorMessage = "Пользователь с такой почтой уже существует", AdditionalFields = "Email,Id")]
    public string Email {get; set;}
    [Required]
    [Remote(action:"CheckBirthDate", controller:"Validation", ErrorMessage = "Возраст не может быть больше 100 и находиться в будущем")]
    public DateOnly DateOfBirth {get; set;}
}