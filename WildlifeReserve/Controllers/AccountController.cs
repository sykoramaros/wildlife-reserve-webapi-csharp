using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WildlifeReserve.DTO;
using WildlifeReserve.Models;

namespace WildlifeReserve.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase {
    private UserManager<AppUser> userManager;
    private SignInManager<AppUser> signInManager;
    private IConfiguration configuration;

// Konstruktor třídy, který přijímá `UserManager` a `SignInManager` prostřednictvím Dependency Injection.
    // public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
    //     this.userManager = userManager; // ulozeni userManager pro praci s uzivateli
    //     this.signInManager = signInManager; // ulozeni signInManager pro autentifikaci uzivatelu
    //     this.configuration = configuration;
    // }

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration) {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
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
                      return Ok(new { message = "Login successful", returnUrl = loginDto.ReturnUrl ?? "/" });
                  } 
                  // Tento přístup zajistí, že pokud je ReturnUrl prázdný nebo null, bude použita hodnota "/" (což může být defaultní stránka nebo kořenová stránka aplikace).
            } 
        }
        return Unauthorized(new { message = "Invalid login or password"});
    }
    
    // Jwt token login start
    [HttpPost("jwt-login")]
    [AllowAnonymous]
    public async Task<IActionResult> JwtLogin([FromBody] LoginDto loginDto) {
        if (ModelState.IsValid) {
            AppUser appUser = await userManager.FindByNameAsync(loginDto.UserName);
            if (appUser != null) {
                Microsoft.AspNetCore.Identity.SignInResult signInResult
                    = await signInManager.PasswordSignInAsync(appUser, loginDto.Password, isPersistent: false,
                        lockoutOnFailure: false);
                if (signInResult.Succeeded) {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, appUser.UserName),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new
                    {
                        token = jwtToken, message = "Jwt Login Succesfull", returnUrl = loginDto.ReturnUrl ?? "/"
                    });
                }
            }
        }
        return Unauthorized(new { message = "Invalid login or password" });
    }
    // Jwt token login stop
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await signInManager.SignOutAsync();
        return Ok(new { message = "Logout successful", returnUrl = "/login" });
    }
    
    [HttpGet("access-denied")]
    public IActionResult AccessDenied() {
        return Unauthorized(new { message = "Access denied", returnUrl = "/forbidden" });
    }
}