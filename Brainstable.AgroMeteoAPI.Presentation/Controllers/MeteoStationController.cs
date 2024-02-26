using Brainstable.AgroMeteoAPI.Service.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [Route("api/[meteostations]")]
    [ApiController]
    public class MeteoStationController : ControllerBase
    {
        private readonly IServiceManager service;

        public MeteoStationController(IServiceManager service) => this.service = service;

        [HttpGet]
        public IActionResult GetMeteoStations()
        {
            try
            {
                var meteoStations = service.MeteoStationService.GetAllMeteoStations(trackChanges: false);
                
                return Ok(meteoStations);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
