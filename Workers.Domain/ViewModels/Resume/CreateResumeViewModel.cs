using System.ComponentModel.DataAnnotations;

namespace Workers.Domain.ViewModels.Resume;

public class CreateResumeViewModel
{
    public long Id { get; set; }

    [Display(Name = "Дата создания")]
    public DateTime DateCreated { get; set; }
        
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Укажите имя")]
    [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
    public string FirstName { get; set; }
        
    [Display(Name = "Фамилия")]
    [MaxLength(50, ErrorMessage = "Фамилия должно иметь длину меньше 50 символов")]
    [MinLength(2, ErrorMessage = "Фамилия должно иметь длину больше 2 символов")]
    public string LastName { get; set; }
        
    [Display(Name = "Отчество")]
    [MaxLength(50, ErrorMessage = "Отчество должно иметь длину меньше 50 символов")]
    [MinLength(2, ErrorMessage = "Отчество должно иметь длину больше 2 символов")]
    public string MiddleName { get; set; }
        
    public string IdUser { get; set; }
}