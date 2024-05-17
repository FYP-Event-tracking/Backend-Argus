using ArgusBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArgusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userDbContext.Users;
        }

        [HttpGet("username/{userName}")]
        public async Task<ActionResult<User>> GetByUserName(string userName)
        {
            var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("userid/{userId}")]
        public async Task<ActionResult<User>> GetById(string userId)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> Update(string userId, User updatedUser)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            
            user.UserName = updatedUser.UserName;
            user.UserType = updatedUser.UserType;
            user.UserAddress = updatedUser.UserAddress;
            user.UserTelephone = updatedUser.UserTelephone;

            _userDbContext.Users.Update(user);
            await _userDbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<User>> Delete(string userId)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}