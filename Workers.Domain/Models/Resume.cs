using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workers.Domain.Enum;

namespace Workers.Domain.Models;

public class Resume
{
    [Key]
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public Faculty Faculty { get; set; }
    public List<Tag> Tags { get; set; }
    public byte[]? Image { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Experience { get; set; }
    public string? AdditionalLinks { get; set; }
    public string? Mail { get; set; }
    public string? Phone { get; set; }
    public string UserId { get; set; }
    
    public User User { get; set; }
}