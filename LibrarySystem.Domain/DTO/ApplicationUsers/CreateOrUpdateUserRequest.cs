namespace LibrarySystem.Domain.DTO.ApplicationUsers;
public record CreateOrUpdateUserRequest : RegistersRequest
{
    public CreateOrUpdateUserRequest(string FirstName, string LastName, string UserName, string Email, string Address, string Password, string PhoneNumber,string Role) : base(FirstName, LastName, UserName, Email, Address, Password, PhoneNumber)
    {
        this.Role = Role;
    }

    public string Role { get; }
}
