using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalWebpage.Models;
using PersonalWebpage.Service;
using PersonalWebpage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebpage.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]   
    public class StopsController: Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger, GeoCoordsService coordsService )
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                //var trip = _repository.GetAllTrips();
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to Get stops: {ex}");
            }

            return BadRequest("Failed to Get stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                //Check if VM is valid
                if(ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stops>(vm);

                    //Lookup Geocodes

                    var result = await _coordsService.GeoCoordsAsync(newStop.Name);
                    if(!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Logitude;
                        //Save it DB
                        _repository.AddStop(tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}",
                            Mapper.Map<StopViewModel>(newStop));
                        }

                    }                  
                    
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get stops: {ex}");
            }

            return BadRequest("Failed to Get stops");
        }

    }
}
