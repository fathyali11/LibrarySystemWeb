namespace LibrarySystem.Domain.DTO.Roles;
public record RoleWithPermissionsResponse(
    string Id,
    string Name,
    IEnumerable<string> Permissions
) : RoleReponse(Id, Name);