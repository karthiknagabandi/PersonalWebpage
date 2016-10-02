using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalWebpage.Models;
using PersonalWebpage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebpage.Controllers.Api
{
    //[Route("api/trips")]
    public class TripsController: Controller
    {
        private ILogger _logger;
        private IWorldRepository _repository;

        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json(new Trip() { Name = "My Trip" });
        //}

        // Using IActionResult because it helps in error handling
        //JsonResult makes it difficult to handle errors. 

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            //Example for Error Handling using IActionResult: if (true) return BadRequest("Bad things Happen");
            try
            {
                var results = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all trips:{ex}");
                return BadRequest("Bad Request");
            }
            
        }

        //[FromBody]-Providing from where the data is coming from -- In PostMan (the data is entered in the Body section)
        [HttpPost("api/trips")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {

            if (ModelState.IsValid)
            {
                //Save data to database
                // we cant save the Viewmodel to DB we have to get the actual trip object to save it to the database
                // AutoMapper: nuget package that helps in getting the object from the viewmodel
                //Mapping the view to the viewmodel is handled by the Auto Mapper
                //Startup should have a configuration to handle this mapping.
                // Trip object, which is view is generated from the theTrip which is viewModel

                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);
                //return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));


                if (await _repository.SaveChangesAsync())
                {
                    //created should have the data as viewmodel data -- so converting the newTrip which has the trip object to ViewModel data 
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save data to DB");
                }

            }
            return BadRequest("Failed to save data to DB");
        }

    }
}
