using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelService_ArgusBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ModelService_ArgusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ModelController> _logger;

        public ModelController(HttpClient httpClient, ILogger<ModelController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Model>> Create(Model model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.PostAsync("http://track-and-count-service-api:8006/submit", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Submit API responded with error: {response.StatusCode}, Content: {errorContent}");
                    return StatusCode((int)response.StatusCode, "Error occurred while calling the submit API.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var logResponse = JsonSerializer.Deserialize<Log>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var logJsonContent = JsonSerializer.Serialize(logResponse);
                var logContent = new StringContent(logJsonContent, Encoding.UTF8, "application/json");

                var logResponseMessage = await _httpClient.PostAsync("http://log-service-api:8080/api/Log", logContent);

                if (!logResponseMessage.IsSuccessStatusCode)
                {
                    var logErrorContent = await logResponseMessage.Content.ReadAsStringAsync();
                    _logger.LogError($"Log API responded with error: {logResponseMessage.StatusCode}, Content: {logErrorContent}");
                    return StatusCode((int)logResponseMessage.StatusCode, "Error occurred while sending log data to the second API.");
                }

                return Ok(logResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}
