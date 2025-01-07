namespace WildlifeReserve.DTO;

public class UserDto {
    // spravce zaregistruje uzivatele (uzivatel se nemuze klasicky zaregistrovat sam) registrace pres backend
    // id se databazi vygeneruje sama
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
}