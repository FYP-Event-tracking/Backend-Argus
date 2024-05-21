using ArgusBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArgusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogDbContext _logDbContext;

        public LogController(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Log>> Create(Log log)
        {
            await _logDbContext.Logs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
