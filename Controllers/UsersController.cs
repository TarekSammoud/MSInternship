using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSS.API.Data;
using MSS.API.DTO.Output;
using MSS.API.Models;

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

        [HttpGet]
        [Route("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            {
                var users = await ctx.Users.ToListAsync();
                return Ok(users);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<AddUserOutput> AddUser(User us)
        {
            AddUserOutput userout = new AddUserOutput
            {
                resultCode = 50
            };



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
            

            return userout; 
        }


    }
}
