using System.ComponentModel.DataAnnotations;

namespace GreenZones.Models.ViewModels
{
    public class AddShotTypeViewModel
    {
        public int UserId { get; set; }
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "Please enter a shot type.")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 8.")]
        public string? ShotType { get; set; }
    }
}
