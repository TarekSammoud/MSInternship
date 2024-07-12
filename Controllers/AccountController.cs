using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSS.API.DTO.Account;
using MSS.API.Models;
using MSS.API.Services;
using System.Security.Claims;


namespace MSS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase

    {

        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;

        public AccountController(JWTService jwtService,
            SignInManager<User> signinManager,
            UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _signinManager = signinManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) { 
                return Unauthorized("Invalid e-mail or password");
            }



            var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            { 
                return Unauthorized("Invalid e-mail or password");
            }

            if (user.EmailConfirmed == false)
            {
                return Unauthorized("Please confirm your email");

            }

            return CreateApplicationUserDto(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.email))
            {
                return BadRequest($"An existing account is using {model.email}, Please try with another Email address");
            }

            var userToAdd = new User
            {
                FirstName = model.firstName.ToLower(),
                LastName = model.lastName.ToLower(),
                Email = model.email.ToLower(),
                UserName = model.email,
                PhoneNumber = model.phoneNumber,
                Cin = model.cin,
                BirthDay = model.BirthDay,
                //  Password = model.password,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd, model.password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new JsonResult(new { title = "Account Created" , message = "Your account has been created"}));

        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            // Retrieve the username from the current user's claims
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Username claim not found or empty.");
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                // Handle the case where user is not found
                // You might return NotFound or handle it based on your application's requirements.
                return NotFound($"User with username '{userName}' not found.");
            }

            // Convert the user to UserDto
            var userDto = CreateApplicationUserDto(user);

            return userDto;
        }


        #region Private Helper Methods 
        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                Jwt= _jwtService.CreateJWT(user),

            };
        }
        #endregion
    }
}
