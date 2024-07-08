using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSS.API.Data;
using MSS.API.DTO.Output;
using MSS.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MSS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MssolutionsContext ctx; 
        private static IConfiguration config; 
        public UsersController(MssolutionsContext MSCC,IConfiguration conf) {
            ctx = MSCC;
            config = conf;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            {
                var users = await ctx.Users.ToListAsync();
                return Ok(users);
            }
        }

        [HttpGet("GetUserByNumTel{numTel}")]
        public async Task<ActionResult<List<User>>> GetUserByNumTel(System.String numTel)
        {
            
                var user = await ctx.Users.FindAsync(numTel);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                return Ok(user);
            
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<List<User>>> AddUser(User us)
        {
            AddUserOutput userout = new AddUserOutput
            {
                resultCode = 50
            };

            /*

            try
            {

                User user = new User
                {
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    NumTel = us.NumTel,
                    Email = us.Email,
                    Password = us.Password,
                    Cin = us.Cin,
                    Address = us.Address,
                    BirthDay = us.BirthDay,
                    CreatedAt   = us.CreatedAt, 
                    UpdatedAt = us.UpdatedAt,
                    Mf = us.Mf,
                    Mcc = us.Mcc,
                    WalletId = us.WalletId,
                    Roles = us.Roles,
                }; 

                ctx.Add(user);
                await ctx.SaveChangesAsync();
            

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
            */

            ctx.Users.Add(us);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok(await ctx.Users.ToListAsync()); 
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(User us)
        {


            var user = await ctx.Users.FindAsync(us.NumTel);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            user.Address= us.Address;
            user.LastName= us.LastName;
            user.FirstName= us.FirstName;
            user.Email= us.Email;
            user.Cin = us.Cin;
            user.Mcc = us.Mcc;
            user.Mf = us.Mf;
            user.UpdatedAt = DateTime.Now;
            user.BirthDay = us.BirthDay;
            user.NumTel = us.NumTel;
            user.Password = us.Password;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(user);

        }

        [HttpDelete("DeleteUser{numTel}")]
        public async Task<ActionResult> DeleteUser(System.String numTel)
        {
            var dbUser = await ctx.Users.FindAsync(numTel);
            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            ctx.Users.Remove(dbUser);

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }


    }
}
