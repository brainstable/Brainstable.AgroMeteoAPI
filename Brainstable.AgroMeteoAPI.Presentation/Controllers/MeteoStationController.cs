using Brainstable.AgroMeteoAPI.Presentation.ActionFilters;
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
        public async Task<IActionResult> GetMeteoStations()
        {
            var meteoStations = await service.MeteoStationService.GetAllMeteoStationsAsync(trackChanges: false);

            return Ok(meteoStations);
        }

        [HttpGet("{meteoStationId}", Name = "MeteoStationById")]
        public async Task<IActionResult> GetMeteoStation(string meteoStationId)
        {
            var meteoStation = await service.MeteoStationService.GetMeteoStationAsync(meteoStationId, false);
            
            return Ok(meteoStation);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateMeteoStation([FromBody] MeteoStationForCreationDto meteoStation)
        {
            var createdMeteoStation = await service.MeteoStationService.CreateMeteoStationAsync(meteoStation);

            return CreatedAtRoute("MeteoStationById", new { meteoStationId = createdMeteoStation.MeteoStationId },
                createdMeteoStation);
        }

        [HttpGet("collection/({ids})", Name = "MeteoStationCollection")]
        public async Task<IActionResult> GetMeteoStationCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute(Name = "ids")]IEnumerable<string> ids)
        {
            var meteoStations = await service.MeteoStationService.GetByIdsAsync(ids, false);

            return Ok(meteoStations);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateMeteoStationCollection([FromBody] IEnumerable<MeteoStationForCreationDto> meteStationCollection)
        {
            var result = await service.MeteoStationService.CreateMeteoStationCollectionAsync(meteStationCollection);

            return CreatedAtRoute("MeteoStationCollection", new { result.ids }, result.meteoStations);
        }

        [HttpDelete("{meteoStationId}")]
        public async Task<IActionResult> DeleteMeteoStation(string meteoStationId)
        {
            await service.MeteoStationService.DeleteMeteoStationAsync(meteoStationId, false);

            return NoContent();
        }

        [HttpPut("{meteoStationId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateMeteoStation(string meteoStationId, [FromBody] MeteoStationForUpdateDto meteoStation)
        {
            await service.MeteoStationService.UpdateMeteoStationAsync(meteoStationId, meteoStation, true);

            return NoContent();
        }
    }
}
