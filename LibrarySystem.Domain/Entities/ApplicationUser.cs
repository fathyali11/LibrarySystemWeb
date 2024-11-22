
using Microsoft.AspNetCore.Identity;

namespace LibrarySystem.Domain.Entities;
public class ApplicationUser:IdentityUser
{
    public string FirstName {  get; set; }=string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Address {  get; set; }=string.Empty;
    // Navigation properties
    public ICollection<Order> ?Orders { get; set; }
    public ICollection<Payment> ?Payment { get; set; }
}