namespace GreenZones.Models.ViewModels
{
    public class EditShotTypeViewModel
    {
        public int UserId { get; set; }
        public int ShotTypeId { get; set; }
        public string? DisplayName { get; set; }
        public string? ShotType { get; set; }
    }
}
