namespace GreenZones.Models;

public partial class UserInfo
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleInitial { get; set; }

    public string? Email { get; set; }

    public bool Active { get; set; }

    public DateTime? LastLogin { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }
}
