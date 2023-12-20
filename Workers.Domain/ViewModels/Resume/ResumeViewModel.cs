using Workers.Domain.Enum;
using Workers.Domain.Models;

namespace Workers.Domain.ViewModels.Resume;

public class ResumeViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public byte[]? Image { get; set; }
    public Faculty Faculty { get; set; }
    public List<Tag> Tags { get; set; }
    public DateTime DateCreated { get; set; }
}