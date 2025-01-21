namespace GreenZones.Models.ViewModels
{
    public class IndexViewModel
    {
        public string? Display_name { get; set; }
        public int Total_Shot_Percentage { get; set; }
        public int Total_Shot_Made { get; set; }
        public int? longest_streak { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}
