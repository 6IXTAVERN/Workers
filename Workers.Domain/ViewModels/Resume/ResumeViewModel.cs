using Workers.Domain.Enum;

namespace Workers.Domain.ViewModels.Resume;

public class ResumeViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public byte[]? Image { get; set; }
    public Faculty Faculty { get; set; }
    public List<string>? Tags { get; set; }
    public DateTime DateCreated { get; set; }
}