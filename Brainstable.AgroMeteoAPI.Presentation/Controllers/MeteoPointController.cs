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
        [Route("day/{date:DateTime}")]
        public IActionResult GetMeteoPoint(string meteoStationId, DateOnly date)
        {
            var meteoPoint = service.MeteoPointService.GetMeteoPoint(meteoStationId, date, false);

            return Ok(meteoPoint);
        }

        [HttpGet]
        [Route("talldays")]
        public IActionResult GetAllDaysTemperature(string meteoStationId)
        {
            var allDays = service.MeteoPointService.GetAllDaysTemperature(meteoStationId, false);

            return Ok(allDays);
        }
    }
}
