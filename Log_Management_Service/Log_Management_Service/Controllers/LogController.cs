using LogService_ArgusBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogService_ArgusBackend.Controllers
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

        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetLogs()
        {
            return _logDbContext.Logs;
        }

        [HttpPost]
        public async Task<ActionResult<Log>> Create(Log log)
        {
            await _logDbContext.Logs.AddAsync(log);
            await _logDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("logid/{logId}")]
        public async Task<ActionResult<Log>> GetById(string logId)
        {
            var log = await _logDbContext.Logs.FindAsync(logId);
            if (log == null)
            {
                return NotFound();
            }
            return log;
        }

        [HttpGet("boxid/{boxId}")]
        public async Task<ActionResult<Log>> GetByBoxId(string boxId)
        {
            var log = await _logDbContext.Logs.FirstOrDefaultAsync(x => x.BoxId == boxId);
            if (log == null)
            {
                return NotFound();
            }
            return log;
        }

        [HttpGet("itemType/{itemType}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByItemType(string itemType)
        {
            var logs = await _logDbContext.Logs.Where(x => x.ItemType == itemType).ToListAsync();
            if (logs == null || logs.Count == 0)
            {
                return NotFound();
            }
            return Ok(logs);
        }

        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByUserId(string userId)
        {
            var logs = await _logDbContext.Logs.Where(x => x.UserId == userId).ToListAsync();
            if (logs == null || logs.Count == 0)
            {
                return NotFound();
            }
            return Ok(logs);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByDate(DateTime date)
        {
            var logs = await _logDbContext.Logs
                .Where(x => EF.Functions.DateDiffDay(x.StartTime, date) == 0)
                .ToListAsync();

            if (logs == null || logs.Count == 0)
            {
                return NotFound();
            }
            return Ok(logs);
        }
    }
}
