using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private WorldContext _context;

        public AppController(IMailService mailService, IConfigurationRoot config, WorldContext context)
        {
            _mailService = mailService;
            _config = config;
            _context = context;
        }

        public IActionResult Index()
        {
            //_context is going to query the DB and gets a list of all the trips as trip classes
            // _context will convert  " _context.Trips.ToList() " into a query that is appropriate for the DB that we are using 
            var data = _context.Trips.ToList();
            return View(data);
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
    }
}
