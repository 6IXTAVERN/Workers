using Microsoft.AspNetCore.Identity;

namespace Workers.Domain.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    
    public Resume? Resume { get; set; }
}