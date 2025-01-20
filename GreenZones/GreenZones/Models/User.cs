namespace GreenZones.Models;

/// <summary>
/// store logged in user information
/// </summary>
public partial class User
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = null!;

    public string? Mail { get; set; }

    public string? MobilePhone { get; set; }

    public string UserPrincipalName { get; set; } = null!;

    public string? LoginId { get; set; }

    public string? GivenName { get; set; }

    public string? Surname { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;
}
