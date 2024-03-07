﻿using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
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
    }
}
