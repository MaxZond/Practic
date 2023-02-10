using Microsoft.AspNetCore.Mvc;

namespace Practic_Api_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {   
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name == null)
            { 
                return BadRequest("Null name"); 
            }

            Summaries.Add(name);        
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name) 
        {
            if(index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Bad index");
            }
            if (name == null)
            {
                return BadRequest("Null name");
            }

            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Bad index");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpGet("{index}")]
        public string GetName(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "Bad index";
            }

            return Summaries[index];
        }
    }
}