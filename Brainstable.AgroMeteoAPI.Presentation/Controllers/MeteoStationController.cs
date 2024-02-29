using Brainstable.AgroMeteoAPI.Service.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [Route("api/meteostations")]
    [ApiController]
    public class MeteoStationController : ControllerBase
    {
        private readonly IServiceManager service;

        public MeteoStationController(IServiceManager service) => this.service = service;

        [HttpGet]
        public IActionResult GetMeteoStations()
        {
            var meteoStations = service.MeteoStationService.GetAllMeteoStations(trackChanges: false);

            return Ok(meteoStations);
        }

        [HttpGet("{meteoStationId}")]
        public IActionResult GetMeteoStation(string meteoStationId)
        {
            var meteoStation = service.MeteoStationService.GetMeteoStation(meteoStationId, false);

            return Ok(meteoStation);
        }
    }
}
