using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WildlifeReserve;
using WildlifeReserve.ExternalApis.iNaturalist.Connector;
using WildlifeReserve.ExternalApis.iNaturalist.Services;
using WildlifeReserve.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Pouze pro Web API, bez zobrazení (views).

// Nastavení HttpClientu pro získání pozorování.
builder.Services.AddHttpClient<iNaturalistApiConnector>();

// builder.Services.AddHttpClient<JsonController>();

// Nastavení servisu pro získání seznamu pozorování.
builder.Services.AddScoped<ObservationService>();


// Nastavení DbContext pro připojení k databázi pomocí SQL Serveru.
//propojena lokalni databaze MAMP MySql (pro spravne fungovani musi MAMP bezet)
// LOKALNI MAMP MySql DB
// builder.Services.AddDbContext<ApplicationDbContext>(options => {
//     options.UseMySql(builder.Configuration.GetConnectionString("DBConnection"),new MySqlServerVersion(new Version(10,6, 28)));
// });

// EXTERNI ASP.NET Mariadb
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("MonsterAspDbConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MonsterAspDbConnection"))
    );
});

// Nastavení Identity (pro autentizaci a autorizaci).
builder.Services.AddIdentity<AppUser, IdentityRole>()
    // Používá ApplicationDbContext pro práci s databází pro uživatelské účty.
    .AddEntityFrameworkStores<ApplicationDbContext>()   
    // Přidává výchozí poskytovatele tokenů pro funkce jako obnovení hesla nebo ověření účtu.
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    // Nastavení pro hesla.
    options.Password.RequiredLength = 6; // Minimum 6 znaků
    options.Password.RequireDigit = true; // Heslo musí obsahovat alespon jednu cislici
    options.Password.RequireLowercase = true; // Heslo musí obsahovat alespon jeden maly znak
    options.Password.RequireUppercase = true; // Heslo musí obsahovat alespon jeden velky znak
    options.Password.RequireNonAlphanumeric = false; // Heslo nemusí obsahovat specialní znaky
});

// Konfigurace cookies pro autentizaci aplikace.
builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = "AspNetCore.Identity.Application"; // Nastavuje nazev cookie pro autentizaci
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);  // Cookie bude platit 10 minut
    options.SlidingExpiration = true;   // Umožňuje obnovu platnosti cookie při každém požadavku (tzn. prodlužuje čas do vypršení, když uživatel aktivně používá aplikaci).
}); 

// Nastavení Swaggeru.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Wildlife Reserve API",
        Version = "v1",
        Description = "API pro spravu divoke prirody"
    });
    options.EnableAnnotations();
});

// // Pridani CORS pro vsechny zdroje
// builder.Services.AddCors(options => {
//     options.AddPolicy("AllowAnyOrigin", corsBuilder => {
//         corsBuilder.AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .AllowCredentials();
//     });
// });

// Pridani CORS jen pro localhost:3000
builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost", corsBuilder => {
        corsBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline. Konfigurace HTTP pipeline pozadavku.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();  // Přesměrování na HTTPS pro zajištění bezpečného připojení.

// Aktivace CORS
// app.UseCors("AllowAnyOrigin");
app.UseCors("AllowLocalhost");

app.UseRouting();   // Umožňuje použití routování pro mapování požadavků HTTP na specifické akce kontrolérů.
app.UseAuthentication();    // Aktivuje autentizaci pro ověření uživatele 
app.UseAuthorization();     // Aktivuje autentizaci pro ověření uživatele
app.MapControllers();     // Mapuje kontrolery na URL adresy. Pro Web api neni nutne dovnitr metody neco pridavat


app.Run();  // Spustí aplikaci a zacne zpracovavat HTTP pozadavky

  