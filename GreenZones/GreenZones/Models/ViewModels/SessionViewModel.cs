namespace GreenZones.Models.ViewModels
{
    public class SessionViewModel
    {
        public virtual User? User { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}
