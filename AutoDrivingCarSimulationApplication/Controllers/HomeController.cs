using AutoDrivingCarSimulationApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoDrivingCarSimulationApplication.Service;
using Microsoft.AspNetCore.Components.Forms;
using AutoDrivingCarSimulationApplication.Helpers;

namespace AutoDrivingCarSimulationApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CarSimulationInput simInput)
        {
            if (simInput.Opr == Constants.RunClick && ModelState.IsValid)
            {
                //Validation for the input Parameters
                bool isValidCarPosition = simInput.CurrentPosition != null ? CarSimulationService.validateCurrentPosition(simInput.CurrentPosition) : false ;

                if (isValidCarPosition && simInput.Commands != null && simInput.Commands != "")
                {
                    bool isValidWidthAndHeight = CarSimulationService.validateWidthAndHeight(simInput.Width, simInput.Height, simInput.CurrentPosition);

                    if (isValidWidthAndHeight)
                    {
                        if (!CarSimulationService.CommandFormatValid(simInput.Commands))
                        {
                            ////invalid commands
                            ModelState.AddModelError(Constants.InvcommandText, Constants.InvalidExecutionCommandMessage);
                        }
                        else
                        {
                            //Execute the commands and get the car position and direction of the car

                            simInput = CarSimulationService.ExecuteCarAutoDriveCommands(simInput);
                        }
                    }
                    else
                    {
                        ////invalid width and height
                        ModelState.AddModelError(Constants.width, "Invalid input, the car current position " + simInput.CurrentPosition + " which is greater than Width = " + simInput.Width + " and Height = " + simInput.Height);
                    }

                }
                else
                {
                    //invalid car position
                    ModelState.AddModelError(Constants.currentPos, Constants.InvalidCarPosition);
                }
            }

            if (simInput != null)
            {
                ViewData["Positionresult"] = simInput.Positionresult;
                ViewData["Directionresult"] = simInput.Directionresult;
            }
            else
            {
                ViewData["Positionresult"] = null;
                ViewData["Directionresult"] = null;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}