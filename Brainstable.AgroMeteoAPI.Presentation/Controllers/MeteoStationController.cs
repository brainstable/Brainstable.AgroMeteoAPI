using Brainstable.AgroMeteoAPI.Presentation.ModelBinders;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [Route("meteostations")]
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

        [HttpGet("{meteoStationId}", Name = "MeteoStationById")]
        public IActionResult GetMeteoStation(string meteoStationId)
        {
            var meteoStation = service.MeteoStationService.GetMeteoStation(meteoStationId, false);
            
            return Ok(meteoStation);
        }

        [HttpPost]
        public IActionResult CreateMeteoStation([FromBody] MeteoStationForCreationDto meteoStation)
        {
            if (meteoStation is null)
                return BadRequest("MeteoStationForCreationDto object is null");

            var createdMeteoStation = service.MeteoStationService.CreateMeteoStation(meteoStation);

            return CreatedAtRoute("MeteoStationById", new { meteoStationId = createdMeteoStation.MeteoStationId },
                createdMeteoStation);
        }

        [HttpGet("collection/({ids})", Name = "MeteoStationCollection")]
        public IActionResult GetMeteoStationCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute(Name = "ids")]IEnumerable<string> ids)
        {
            var meteoStations = service.MeteoStationService.GetByIds(ids, false);

            return Ok(meteoStations);
        }

        [HttpPost("collection")]
        public IActionResult CreateMeteoStationCollection([FromBody] IEnumerable<MeteoStationForCreationDto> meteStationCollection)
        {
            var result = service.MeteoStationService.CreateMeteoStationCollection(meteStationCollection);

            return CreatedAtRoute("MeteoStationCollection", new { result.ids }, result.meteoStations);
        }
    }
}
