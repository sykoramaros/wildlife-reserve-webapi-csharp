using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildlifeReserve.DTO;
using WildlifeReserve.Models;

namespace WildlifeReserve.Controllers;

// ControllerBase je pro backend Web API a nepracuje s Views narozdil od Controller.
[Route("api/[controller]")]
[ApiController]
[Authorize (Roles = "Admin, Director")]
public class UsersController : ControllerBase {
    private UserManager<AppUser> userManager;
    private IPasswordHasher<AppUser> passwordHasher;
    private IPasswordValidator<AppUser> passwordValidator;
    
    // konstruktor
    public UsersController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, IPasswordValidator<AppUser> passwordValidator) {
        this.userManager = userManager; // ulozeni userManager
        this.passwordHasher = passwordHasher; // ulozeni passwordHasher hashovani hesel
        this.passwordValidator = passwordValidator; // ulozeni passwordValidator pro kontrolu hesla
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetUsers() {
        var users = await userManager.Users.ToListAsync();
        return Ok(users);   // Vrátí 200 OK s seznamem uživatelů
    }

    [HttpGet("getBy/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email) {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) {
            return NotFound(); // 404 NotFound, pokud neexistuje uživatel s danou id
        } else {
            return Ok(user);
        }
    }
    
    [HttpPost("getBy/{id}")]
    public async Task<IActionResult> GetUserById(string id) {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) {
            return NotFound(); // 404 NotFound, pokud neexistuje uživatel s danou id
        }
        return Ok(user); // Vrátí 200 OK s daty uživatele
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto newUser) {
        if (ModelState.IsValid) {
            // kontrola jestli uzivetel uz neexistuje
            var existingUser = await userManager.FindByEmailAsync(newUser.Email);
            if (existingUser != null) {
                return BadRequest("ApiUser already exists"); // 400 BadRequest, pokud uživatel jiz existuje
            }
            // vytvoreni noveho uzyvatele
            AppUser appUser = new AppUser {
                UserName = newUser.Name,
                Email = newUser.Email
            };
            IdentityResult result = await userManager.CreateAsync(appUser, newUser.Password);
            if (result.Succeeded) {
                return CreatedAtAction(nameof(GetUserById),
                    new { id = appUser.Id }, appUser); // Vrátí 201 s URL k novemu uživateli
            } else {
                return BadRequest(result.Errors); // 400 BadRequest, pokud došlo k chybám při vytváření uživatele
            }
        }
        return BadRequest(ModelState);  // 400 BadRequest, pokud model neni validni
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> EditUser(string id, [FromBody] UserDto editedUser) {
        var userToEdit = await userManager.FindByIdAsync(id);
        if (userToEdit == null) {
            return NotFound();    // 404 NotFound, pokud neexistuje uživatel s danou id
        }
        userToEdit.UserName = editedUser.Name;
        userToEdit.Email = editedUser.Email;
        IdentityResult result = await userManager.UpdateAsync(userToEdit);
        if (result.Succeeded) { 
            return Ok(); // Vrátí 200 OK
        } else {
            return BadRequest(result.Errors);   // 400 BadRequest
        }
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id) {
        var userToDelete = await userManager.FindByIdAsync(id);
        if (userToDelete == null) {
            return NotFound();    // 404 NotFound, pokud neexistuje uživatel s danou id
        }
        IdentityResult result = await userManager.DeleteAsync(userToDelete);
        if (result.Succeeded) { 
            return Ok();    // Vrátí 200 OK
        } else { 
            return BadRequest(result.Errors);   // 400 BadRequest
        }
    }
}