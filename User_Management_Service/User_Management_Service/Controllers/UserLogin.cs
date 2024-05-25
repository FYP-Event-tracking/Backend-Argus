using UserService_ArgusBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UserService_ArgusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly UserLoginDbContext _userLoginDbContext;

        public UserLoginController(UserLoginDbContext userLoginDbContext)
        {
            _userLoginDbContext = userLoginDbContext;
        }

        [HttpGet("userid/{userId}")]
        public async Task<ActionResult<UserLogin>> GetById(string userId)
        {
            var user = await _userLoginDbContext.UserLogin.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserLogin>> Create(UserLogin user)
        {
            await _userLoginDbContext.UserLogin.AddAsync(user);
            await _userLoginDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserLogin>> Update(string userId, UserLogin updatedUser)
        {
            var user = await _userLoginDbContext.UserLogin.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            
            user.UserPassword = updatedUser.UserPassword;
            user.UserType = updatedUser.UserType;

            _userLoginDbContext.UserLogin.Update(user);
            await _userLoginDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserLogin>> Delete(string userId)
        {
            var user = await _userLoginDbContext.UserLogin.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            _userLoginDbContext.UserLogin.Remove(user);
            await _userLoginDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}