namespace GreenZones.Models;

/// <summary>
/// each session records shot counts
/// </summary>
public partial class Session
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ShotTypeId { get; set; }
    public int Makes { get; set; }

    public int TotalShots { get; set; }

    public int? Streak { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual ShotType ShotType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
