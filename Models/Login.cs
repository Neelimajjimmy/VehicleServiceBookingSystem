using Microsoft.AspNetCore.Mvc;
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
        [Remote(action: "checkOldPwd",
            controller: "PwdValidation",
            ErrorMessage = "Old password is incorrect.")]
        public string? password { get; set; }
        [Required(ErrorMessage = "Required")]
        [Remote(action: "checkNewPwd",
            controller: "PwdValidation",
             AdditionalFields = nameof(password))]
        public string? newpwd { get; set; }
        [Required(ErrorMessage ="Required")]
        [Compare("newpwd",ErrorMessage ="Passwords do not match")]
        public string? confirmpwd { get; set; }
    }
}
