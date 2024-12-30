namespace WildlifeReserve.Models;
// puvodni RoleModifications.cs
public class UserRoleModifications {
    public string RoleName { get; set; }
    // Název role, který se přenáší do backendu. Například pro přiřazení nebo odebrání uživatelů této roli.
    public string RoleId { get; set; }
    // ID role, které identifikuje konkrétní roli v databázi. Používá se k provádění operací na této roli.
    public string[]? AddIds { get; set; }
    // Pole ID uživatelů, kteří mají být přidáni do role. 
    // Uživatelé jsou identifikováni svými unikátními ID.
    public string[]? DeleteIds { get; set; }
    // Pole ID uživatelů, kteří mají být odebráni z role. 
    // Stejně jako `AddIds` obsahuje ID uživatelů.
}

// Tato třída je užitečná zejména při práci s checkboxy v uživatelském rozhraní,
// kde uživatel může vybrat, které uživatele chce přidat nebo odebrat z role.