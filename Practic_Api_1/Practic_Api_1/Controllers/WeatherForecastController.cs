using Microsoft.AspNetCore.Mvc;
using System;

namespace Practic_Api_1.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {   
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { Id = 1, Date = "06.01.2004", Degree = 22, Location="LA" },
            new WeatherData() { Id = 2, Date = "11.06.2022", Degree = -22, Location = "Arctic" },
            new WeatherData() { Id = 3, Date = "22.12.2020", Degree = -4, Location = "NY" }
        };


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> Get()
        {
            return weatherDatas;
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            if (data == null)
            { 
                return BadRequest("Null name"); 
            }

            for(int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    return BadRequest("Id already exists");
                }
            }
            weatherDatas.Add(data);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(WeatherData data) 
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    weatherDatas[i] = data;
                    return Ok();
                }
            }

            return BadRequest("Id not found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    weatherDatas.RemoveAt(i);
                    return Ok();
                }
            }

            return BadRequest("Id not found");
        }

        [HttpGet("{id}")]
        public IActionResult GetName(int id)
        {
            if (id < 0 || id >= Summaries.Count)
            {
                return BadRequest("Bad index");
            }

            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    return Ok(weatherDatas[i]);
                }
            }

            return BadRequest("Item not found!");
        }

        [HttpGet("find-by-city")]
        public IActionResult GetByCity(string location) 
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Location == location)
                {
                    return Ok("Item found!");
                }
            }

            return BadRequest("Item not found!");    
        }
    }
}