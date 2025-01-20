namespace GreenZones.Models;

public partial class UserBucket
{
    public int Id { get; set; }

    public string? ClientId { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? LastAccessed { get; set; }

    public int? RefillRateSeconds { get; set; }

    public int? BucketCount { get; set; }

    public int? BucketLimit { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
