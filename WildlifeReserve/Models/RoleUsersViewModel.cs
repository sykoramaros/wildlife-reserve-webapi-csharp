using System.Collections;
using Microsoft.AspNetCore.Identity;
namespace WildlifeReserve.Models;
// puvodni RoleEdit.cs
public class RoleUsersViewModel {
    // Uživatele
    public IdentityRole Role { get; set; }
    // Objekt `IdentityRole` představuje roli z knihovny Identity (např. jméno role, její ID).
    public IEnumerable<AppUser> Members { get; set; }
    // Kolekce uživatelů, kteří jsou členy dané role. `AppUser` je vlastní třída uživatele aplikace.
    public IEnumerable<AppUser> NonMembers { get; set; }
    // Kolekce uživatelů, kteří nejsou členy dané role.
}