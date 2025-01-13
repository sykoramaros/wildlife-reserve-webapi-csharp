using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildlifeReserve.Models;

namespace WildlifeReserve.Controllers;
[Route("api/[controller]")]
[ApiController]
// [Authorize (Roles = "Admin, Director")]
public class RolesController : ControllerBase {
    private RoleManager<IdentityRole> roleManager;
    private UserManager<AppUser> userManager;

    // Konstruktor třídy, který přijímá `RoleManager` a `UserManager` prostřednictvím Dependency Injection.
    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    
    // Získání seznamu všech rolí
    [HttpGet("list")]
    public async Task<IActionResult> GetRoles() {
        var roles = await roleManager.Roles.ToListAsync();
        return Ok(roles);
    }
    
    // Vytvoření nové role
    [HttpPost("add")]
    public async Task<IActionResult> CreateRole(string roleName) {
        // kontrola jestli role uz neexistuje
        var existingRole = await roleManager.FindByNameAsync(roleName); 
        if (existingRole != null) { 
            return BadRequest("Role already exists");
        }
        IdentityResult result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded) {
            return Ok(new {message = "Role created successfully"});
        }
        return BadRequest(result.Errors);
    }
    
    // Úprava role (získání členů a nečlenů)
    [HttpGet("getBy/{id}")]
    public async Task<IActionResult>EditRole(string id) {
        IdentityRole roleToEdit = await roleManager.FindByIdAsync(id);
        List<AppUser> members = new List<AppUser>();
        List<AppUser> nonMembers = new List<AppUser>();
    
        if (roleToEdit != null) {
            foreach (AppUser user in userManager.Users) {
                bool isInRole = await userManager.IsInRoleAsync(user, roleToEdit.Name);
                var list = isInRole ? members : nonMembers;
                list.Add(user);
            }
        
            return Ok(new RoleUsersViewModel {
                Role = roleToEdit,
                Members = members,
                NonMembers = nonMembers
            });
        } else {
            return NotFound();
        }
    }

    [HttpPut("modifications")]
    public async Task<IActionResult> EditModificationAsync(UserRoleModifications modification) {
        foreach (string userId in modification.AddIds ?? Array.Empty<string>()) {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null) {
                IdentityResult result = await userManager.AddToRoleAsync(user, modification.RoleName);
                if (!result.Succeeded) {
                    AddModelErrors(result);
                }
            }
        }

        foreach (string userId in modification.DeleteIds ?? Array.Empty<string>()) {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null) {
                IdentityResult result = await userManager.RemoveFromRoleAsync(user, modification.RoleName);
                if (!result.Succeeded) {
                    AddModelErrors(result);
                }
            }
        }
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        return Ok();
    }
    // Metoda pro přidání chyb IdentityResult do ModelState.
    private void AddModelErrors(IdentityResult result) {
        foreach (var error in result.Errors) {
            ModelState.AddModelError(string.Empty, error.Description); // Přidá chyby do ModelState.
        }
    }
    
    // Smazání role
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRole(string id) {
        IdentityRole foundRole = await roleManager.FindByIdAsync(id);
        if (foundRole != null) {
            IdentityResult result = await roleManager.DeleteAsync(foundRole);
            if (result.Succeeded) {
                return Ok(new {message = "Role deleted successfully"}); // HTTP 200
            } else {
                return BadRequest(new {errors = result.Errors, message = "Failed to delete role"}); // kod 400, pokud klient poslal neplatný požadavek (např. chybějící nebo nesprávné údaje) a (result.Errors) pravděpodobně obsahuje detailní popis chyb, které způsobily, že požadavek nebyl zpracován.
            }
        } else {
            return NotFound(new { message = "Role not found"}); // kod 404, pokud požadovaný zdroj nebyl nalezen na serveru a neobsahuje žádné další informace
        }
    }
}