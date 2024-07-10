using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MSS.API.DTO.Account
{
    public class RegisterDto
    {

        [Required]
        public string phoneNumber { get; set; }


        [Required]
        [StringLength(15,MinimumLength = 2 , ErrorMessage = "First Name must be at least {2}, and at maximum {1} characters  ")]
        public String firstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Last Name must be at least {2}, and at maximum {1} characters  ")]
        public String lastName { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string password { get; set; }

        [Required]
        public String cin { get; set; }


        [Required]
        public DateTime BirthDay { get; set; }


    }
}
