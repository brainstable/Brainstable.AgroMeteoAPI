using Brainstable.AgroMeteoAPI.Presentation.ActionFilters;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Brainstable.AgroMeteoAPI.Presentation.Controllers
{
    [Route("meteostations/{meteoStationId}/")]
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
        public async Task<IActionResult> GetAllDaysMeteoPoints(string meteoStationId)
        {
            var allDaysMeteoPoints = await service.MeteoPointService.GetAllDaysMeteoPointsAsync(meteoStationId, false);

            return Ok(allDaysMeteoPoints);
        }

        [HttpGet]
        [Route("{date}", Name = "GetMeteoPointForMeteoStation")]
        public async Task<IActionResult> GetMeteoPoint(string meteoStationId, string date)
        {
            DateOnly dateOnly = DateOnly.Parse(date);
            
            var meteoPoint = await service.MeteoPointService.GetMeteoPointAsync(meteoStationId, dateOnly, false);

            return Ok(meteoPoint);
        }

        [HttpGet]
        [Route("talldays")]
        public async Task<IActionResult> GetAllDaysTemperature(string meteoStationId)
        {
            var allDays = await service.MeteoPointService.GetAllDaysTemperatureAsync(meteoStationId, false);

            return Ok(allDays);
        }

        [HttpGet]
        [Route("days")]
        public async Task<IActionResult> GetMeteoPoint(
            [FromRoute(Name = "meteoStationId")] string meteoStationId,
            [FromQuery(Name = "start")] string startDate,
            [FromQuery(Name = "end")] string endDate)
        {
            DateOnly start = DateOnly.Parse(startDate);
            DateOnly end = DateOnly.Parse(endDate);

            var meteoPoint = await service.MeteoPointService.GetDaysMeteoPointsAsync(meteoStationId, start, end, false);

            return Ok(meteoPoint);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateMeteoPointForMeteoStation(string meteoStationId,
            [FromBody] MeteoPointForCreationDto meteoPoint)
        {
            var meteoPointToReturn = await service.MeteoPointService.CreateMeteoPointForMeteoStationAsync(meteoStationId, meteoPoint, false);

            return CreatedAtRoute("GetMeteoPointForMeteoStation", new { meteoStationId },
                meteoPointToReturn);
        }

        [HttpDelete("{date}")]
        public async Task<IActionResult> DeleteMeteoPointForMeteoSttaion(string meteoStationId, string date)
        {
            DateOnly dateOnly = DateOnly.Parse(date);

            await service.MeteoPointService.DeleteMeteoPointForMeteoStationAsync(meteoStationId, dateOnly, false);

            return NoContent();
        }

        [HttpPut("{date}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateMeteoPointForMeteoStation(string meteoStationId, string date,
            [FromBody] MeteoPointForUpdateDto meteoPoint)
        {
            DateOnly dateOnly = DateOnly.Parse(date);

            await service.MeteoPointService.UpdateMeteoPointForMeteoStationAsync(meteoStationId, dateOnly, meteoPoint, false, true);

            return NoContent();
        }

        [HttpPatch("{date}")]
        public async Task<IActionResult> PartiallyUpdateMeteoPointForMeteoStation(string meteoStationId, string date,
            [FromBody] JsonPatchDocument<MeteoPointForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null");

            DateOnly dateOnly = DateOnly.Parse(date);
             
            var result = await service.MeteoPointService.GetMeteoPointForPatchAsync(meteoStationId, dateOnly, false, true);

            patchDoc.ApplyTo(result.meteoPointToPatch, ModelState);

            TryValidateModel(result.meteoPointToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.MeteoPointService.SaveChangesForPatchAsync(result.meteoPointToPatch, result.meteoPoint);

            return NoContent();
        }
    }
}
