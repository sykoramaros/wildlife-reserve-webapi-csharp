namespace WildlifeReserve.Models;
// puvodni RoleModifications.cs
public class UserRoleModifications {
    public string RoleName { get; set; }
    public string RoleId { get; set; }
    public string[] AddIds { get; set; }
    public string[] DeleteIds { get; set; }
}