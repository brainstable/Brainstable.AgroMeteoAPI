using Brainstable.AgroMeteoAPI.Service.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [Route("api/meteostations/{meteoStationId}/")]
    [ApiController]
    public class MeteoPointController : ControllerBase
    {
        private readonly IServiceManager service;

        public MeteoPointController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("alldays")]
        public IActionResult GetAllDaysMeteoPoints(string meteoStationId)
        {
            var allDaysMeteoPoints = service.MeteoPointService.GetAllDaysMeteoPoints(meteoStationId, false);

            return Ok(allDaysMeteoPoints);
        }

        [HttpGet]
        [Route("day")]
        public IActionResult GetMeteoPoint(
            [FromRoute(Name = "meteoStationId")]string meteoStationId, 
            [FromQuery(Name = "date")]string date)
        {
            DateOnly dateOnly = DateOnly.Parse(date);
            
            var meteoPoint = service.MeteoPointService.GetMeteoPoint(meteoStationId, dateOnly, false);

            return Ok(meteoPoint);
        }

        [HttpGet]
        [Route("talldays")]
        public IActionResult GetAllDaysTemperature(string meteoStationId)
        {
            var allDays = service.MeteoPointService.GetAllDaysTemperature(meteoStationId, false);

            return Ok(allDays);
        }

        [HttpGet]
        [Route("days")]
        public IActionResult GetMeteoPoint(
            [FromRoute(Name = "meteoStationId")] string meteoStationId,
            [FromQuery(Name = "start")] string startDate,
            [FromQuery(Name = "end")] string endDate)
        {
            DateOnly start = DateOnly.Parse(startDate);
            DateOnly end = DateOnly.Parse(endDate);

            var meteoPoint = service.MeteoPointService.GetDaysMeteoPoints(meteoStationId, start, end, false);

            return Ok(meteoPoint);
        }
    }
}
