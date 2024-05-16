using ArgusBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArgusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ArgusDbContext _argusDbContext;

        public UserController(ArgusDbContext argusDbContext)
        {
            _argusDbContext = argusDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _argusDbContext.Users;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<User>> GetById(int customerId)
        {
            var user = await _argusDbContext.Users.FindAsync(customerId);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _argusDbContext.Users.AddAsync(user);
            await _argusDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(User user)
        {
            _argusDbContext.Users.Update(user);
            await _argusDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<User>> Delete(int userId)
        {
            var user = await _argusDbContext.Users.FindAsync(userId);
            _argusDbContext.Users.Remove(user);
            await _argusDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}