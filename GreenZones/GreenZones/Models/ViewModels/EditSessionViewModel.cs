using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GreenZones.Models.ViewModels
{
    public class EditSessionViewModel
    {
        public int UserId { get; set; }
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, 999999)]
        public int Makes { get; set; }

        [Required(ErrorMessage = "Please enter a number.")]
        [Range(0, 999999)]
        public int TotalShot { get; set; }

        [Range(0, 999999)]
        public int? Streak { get; set; }
        public string? DisplayName { get; set; }
        public string? ShotTypeId { get; set; }
        public IEnumerable<SelectListItem>? ShotTypes { get; set; }
    }
}
