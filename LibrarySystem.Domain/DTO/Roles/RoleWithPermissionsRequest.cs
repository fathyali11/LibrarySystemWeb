using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.DTO.Roles;
public record RoleWithPermissionsRequest(string Name, IEnumerable<string> Permissions);