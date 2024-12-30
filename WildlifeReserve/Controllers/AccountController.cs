using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WildlifeReserve.DTO;
using WildlifeReserve.Models;

namespace WildlifeReserve.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase {
    private UserManager<AppUser> userManager;
    private SignInManager<AppUser> signInManager;

// Konstruktor třídy, který přijímá `UserManager` a `SignInManager` prostřednictvím Dependency Injection.
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
        this.userManager = userManager; // ulozeni userManager pro praci s uzivateli
        this.signInManager = signInManager; // ulozeni signInManager pro autentifikaci uzivatelu
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl) {
        LoginDto loginDto = new LoginDto();
        loginDto.ReturnUrl = returnUrl;
        return Ok(loginDto);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    // [ValidateAntiForgeryToken] neni v API potreba
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (ModelState.IsValid) {
            AppUser appUser = await userManager.FindByNameAsync(loginDto.UserName);
            if (appUser != null) {
                  Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
                  if (signInResult.Succeeded) {
                      return Ok(new { message = "Login successful", returnUrl = string.IsNullOrEmpty(loginDto.ReturnUrl) ? "/" : loginDto.ReturnUrl});
                  } 
                  // Tento přístup zajistí, že pokud je ReturnUrl prázdný nebo null, bude použita hodnota "/" (což může být defaultní stránka nebo kořenová stránka aplikace).
            } 
        }
        return Unauthorized(new { message = "Invalid login or password"});
    }
        
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await signInManager.SignOutAsync();
        return Ok(new { message = "Logout successful"});
    }
    
    [HttpGet("access-denied")]
    public IActionResult AccessDenied() {
        return Unauthorized(new { message = "Access denied"});
    }
}