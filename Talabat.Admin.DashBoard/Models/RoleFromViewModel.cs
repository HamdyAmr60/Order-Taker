using System.ComponentModel.DataAnnotations;

namespace Talabat.Admin.DashBoard.Models
{
    public class RoleFromViewModel
    {
        public string? Id { get; set; } 
        [Required]
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
