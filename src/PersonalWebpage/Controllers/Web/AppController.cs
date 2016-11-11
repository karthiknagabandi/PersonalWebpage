using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersonalWebpage.Models;
using PersonalWebpage.Service;
using PersonalWebpage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebpage.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository, 
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_context is going to query the DB and gets a list of all the trips as trip classes
            // _context will convert  " _context.Trips.ToList() " into a query that is appropriate for the DB that we are using
            // -- Commenting out to use Authorization pages --  
            //try
            //{
            //    var data = _repository.GetAllTrips();

            //    return View(data);

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get trips in index page: {ex.Message}");
            //    return Redirect("/error");
            //}

            return View();
        } 

        [Authorize]
        public IActionResult Trips()
        {
            // Creating another page for authorization instead of directly using the index page
            //commenting this to use the angular control
            //try
            //{
            //    var data = _repository.GetAllTrips();

            //    return View(data);

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get trips in index page: {ex.Message}");
            //    return Redirect("/error");
            //}
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            //if(model.Email.Contains("aol.com"))
            //{
            //    ModelState.AddModelError("", "We Dont support AOL");
            //}

            if (ModelState.IsValid)
            {
                _mailService.sendMail(_config["MailSettings:ToAddress"], model.Email, "From Me", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Photography()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }



    }
}
