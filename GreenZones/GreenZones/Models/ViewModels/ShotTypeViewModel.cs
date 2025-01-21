namespace GreenZones.Models.ViewModels
{
    public class ShotTypeViewModel
    {
        public virtual User? User { get; set; }
        public List<ShotType>? ShotTypes { get; set; }
    }
}
