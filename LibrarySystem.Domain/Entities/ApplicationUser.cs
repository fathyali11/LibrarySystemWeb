
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Entities;
public class ApplicationUser:IdentityUser
{
    public string FirstName {  get; set; }=string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Address {  get; set; }=string.Empty;
    public bool IsActive { get; set; } = true;
    // Navigation properties
    public ICollection<Order> ?Orders { get; set; }
    public ICollection<Payment> ?Payments { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}