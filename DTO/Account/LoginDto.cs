using System.ComponentModel.DataAnnotations;

namespace MSS.API.DTO.Account
{
    public class LoginDto
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
