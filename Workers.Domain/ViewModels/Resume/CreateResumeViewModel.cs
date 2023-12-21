using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workers.Domain.Enum;

namespace Workers.Domain.ViewModels.Resume;

public class CreateResumeViewModel
{
    public long Id { get; set; }
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Укажите ваше имя")]
    //[MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
    //[MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
    public string FirstName { get; set; }
        
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Укажите вашу фамилию")]
    //[MaxLength(50, ErrorMessage = "Фамилия должно иметь длину меньше 50 символов")]
    //[MinLength(2, ErrorMessage = "Фамилия должно иметь длину больше 2 символов")]
    public string LastName { get; set; }
        
    [Display(Name = "Отчество")]
    [Required(ErrorMessage = "Укажите ваше отчество")]
    //[MaxLength(50, ErrorMessage = "Отчество должно иметь длину меньше 50 символов")]
    //[MinLength(2, ErrorMessage = "Отчество должно иметь длину больше 2 символов")]
    public string MiddleName { get; set; }
    [Required(ErrorMessage = "Укажите факультет")]
    public Faculty SelectedFaculty { get; set; }
    public List<Faculty>? Faculties { get; set; }
    //[NotMapped]
    public string UserId { get; set; }
}