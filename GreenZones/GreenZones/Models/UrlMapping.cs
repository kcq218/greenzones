namespace GreenZones.Models;

public partial class UrlMapping
{
    public int Id { get; set; }

    public string LongUrl { get; set; } = null!;

    public int KeyId { get; set; }

    public string HashValue { get; set; } = null!;

    public int? UserId { get; set; }

    public bool Active { get; set; }

    public DateTime? LastAccessed { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }
}
