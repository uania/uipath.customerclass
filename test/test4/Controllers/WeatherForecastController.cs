using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;

namespace test4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetContentType(string fileName)
        {
            
            const string DefaultContentType = "application/octet-stream";

            //file extensions 和 MIME 的映射 provider
            var mimeProvider = new FileExtensionContentTypeProvider();
            if (!mimeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = DefaultContentType;
            }

            return contentType;
        }
    }
}
