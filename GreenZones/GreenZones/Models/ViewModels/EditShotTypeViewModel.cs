using System.ComponentModel.DataAnnotations;

namespace GreenZones.Models.ViewModels
{
    public class EditShotTypeViewModel
    {
        public int ShotTypeId { get; set; }
        public int UserId { get; set; }
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "Please enter a shot type.")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string? ShotType { get; set; }
    }
}
