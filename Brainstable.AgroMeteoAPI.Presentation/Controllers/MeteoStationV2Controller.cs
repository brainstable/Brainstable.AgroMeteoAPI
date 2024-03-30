using Asp.Versioning;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [ApiVersion("2.0")]
    [Route("{v:apiVersion}/meteostations")]
    [ApiController]
    public class MeteoStationV2Controller : ControllerBase
    {
        private readonly IServiceManager service;

        public MeteoStationV2Controller(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeteoStations()
        {
            var meteoStations = await service.MeteoStationService.GetAllMeteoStationsAsync(false);

            var meteoStationsV2 = meteoStations.Select(x => $"{x.Name} v2");

            return Ok(meteoStationsV2);
        }
    }
}
