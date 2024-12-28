using Microsoft.AspNetCore.Identity;

namespace WildlifeReserve.Models;

public class AppUser: IdentityUser {    // dedi z IdentityUser spoustu vlastnosti
    // moznost pridani propojeni s jinou tabulkou (department, ...)
}