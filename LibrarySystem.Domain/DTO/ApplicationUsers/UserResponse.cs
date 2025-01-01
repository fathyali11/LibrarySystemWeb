namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record UserResponse
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string Address { get; init; }
    public string PhoneNumber { get; init; }
    public string Role { get; set; }
    public bool IsActive { get; init; }
    public UserResponse() { }
    public UserResponse(string id, string firstName, string lastName, string email, string userName, string address, string phoneNumber, string role,bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        Address = address;
        PhoneNumber = phoneNumber;
        Role = role;
        IsActive = isActive;
    }
}
