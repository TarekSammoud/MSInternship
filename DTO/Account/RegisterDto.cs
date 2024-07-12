using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MSS.API.DTO.Account
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Phone number is required")]
        public string phoneNumber { get; set; }


        [Required(ErrorMessage = "First name is required")]
        [StringLength(15,MinimumLength = 2 , ErrorMessage = "First Name must be at least {2}, and at maximum {1} characters  ")]
        public String firstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Last Name must be at least {2}, and at maximum {1} characters  ")]
        public String lastName { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression("^[\\w._%+-]+@[\\w.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(300, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string password { get; set; }

        [Required(ErrorMessage = "CIN is required")]
        public String cin { get; set; }


        [Required(ErrorMessage = " Birthday is required")]
        public DateTime BirthDay { get; set; }


    }
}
