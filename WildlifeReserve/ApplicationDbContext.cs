using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WildlifeReserve.Models;

namespace WildlifeReserve;

public class ApplicationDbContext : IdentityDbContext<AppUser> {    // Dědění z IdentityDbContext pro práci s Identity
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
}