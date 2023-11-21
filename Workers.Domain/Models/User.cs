using Microsoft.AspNetCore.Identity;

namespace Workers.Domain.Models;

public class User // заимплементить насследование от IdentityUser
{
    public string NickName { get; set; }
    public Resume UserResume { get; set; }
}