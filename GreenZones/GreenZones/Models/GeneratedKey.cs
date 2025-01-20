namespace GreenZones.Models;

public partial class GeneratedKey
{
    /// <summary>
    /// pk incremental int
    /// </summary>
    public int Id { get; set; }

    public string HashValue { get; set; } = null!;

    public int? UrlId { get; set; }

    public bool Active { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }
}
