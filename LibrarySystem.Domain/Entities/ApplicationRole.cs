using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Entities;
public class ApplicationRole:IdentityRole
{
    public bool IsMember { get; set; }
}
