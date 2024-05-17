using ArgusBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetById(string userId)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(User user)
        {
            _userDbContext.Users.Update(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<User>> Delete(string userId)
        {
            var user = await _userDbContext.Users.FindAsync(userId);
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}