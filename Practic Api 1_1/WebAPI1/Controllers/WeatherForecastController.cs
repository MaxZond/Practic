using Microsoft.AspNetCore.Mvc;

namespace WebAPI1.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public List<string> Get()
        {
            return Summaries;
        }
        
        [HttpPost]
        public IActionResult Add(string name)
        {
            if(name.Length < 1)
            {
                return BadRequest("Write new name");
            }
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name) 
        {
            if(index<0 || index >= Summaries.Count)
            {
                return BadRequest("Bad index");
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
        public string Get(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "Bad index";
            }
            return Summaries[index];
        }

        [HttpGet("find-by-name")]
        public int GetString(string name)
        {
            return Summaries.Count(a => a == name);
        }

        [HttpGet("sort")]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy == null) return Ok(Summaries);
            if (sortStrategy == 1)
            {
                Summaries.Sort();
                return Ok(Summaries);
            }    
            if (sortStrategy == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();
                return Ok(Summaries);
            }
            else
            {
                return BadRequest("Invalid parameter value sortStrategy");
            }
        }
    }
}