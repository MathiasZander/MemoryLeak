using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MemoryLeak.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<LogMessage> Get()
        {
            var message = new StringBuilder();
            for (int i = 0; i < 10_000; i++)
            {
                message.AppendLine(Guid.NewGuid().ToString());
            }

            var logMessage = new LogMessage
            {
                Message = message.ToString()
            };
            var logMessages = new List<LogMessage>( );


            for (int i = 0; i < 100; i++)
            {
                logMessages.Add(logMessage);
            }

            return logMessages;
        }

        public class LogMessage
        {
            public string Message { get; set; }
        }
    }
}