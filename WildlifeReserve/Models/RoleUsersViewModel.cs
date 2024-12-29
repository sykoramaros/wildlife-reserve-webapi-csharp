using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace WildlifeReserve.Models;
// puvodni RoleEdit.cs
public class RoleUsersViewModel {
    // Uživatele
    public IdentityRole Role { get; set; }

    public IEnumerable<AppUser> Members { get; set; }
    
    public IEnumerable<AppUser> NonMembers { get; set; }
    
}