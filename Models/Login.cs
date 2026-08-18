using System.ComponentModel.DataAnnotations;

namespace project3VehicleServiceBookingApp.Models
{
    public class Login
    {
        public string username {  get; set; }
        public string password { get; set; }
    }


    public class PasswordChangeDto {
        [Required]
        public string? password { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? newpwd { get; set; }
        [Required(ErrorMessage ="Required")]
        [Compare("newpwd",ErrorMessage ="Passwords do not match")]
        public string? confirmpwd { get; set; }
    }
}
