using AutoDrivingCarSimulationApplication.Helpers;
using AutoDrivingCarSimulationApplication.Models;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AutoDrivingCarSimulationApplication.Service
{
    public class CarSimulationService
    {
        /*Validating current car position format (x,y) axis is valid 
          Validating the width and height value should be greater than x and y 
         */
        public static bool validateCurrentPosition(string currentPosition)
        {
            try
            {
                var isBracketExists = currentPosition.StartsWith("(") && currentPosition.EndsWith(")");
                int x, y;
                if (isBracketExists)
                {
                    currentPosition = currentPosition.Substring(1, (currentPosition.Length - 2)); // removing brackets and validating
                    if (currentPosition.Contains(","))
                    {
                        var axis = currentPosition.Split(',');
                        if (axis.Length == 2)
                        {
                            bool xisNumber = int.TryParse(axis[0], out x);
                            bool yisNumber = int.TryParse(axis[1], out y);

                            if (xisNumber && yisNumber)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return false;
            }

        }

        //execute command format valid
        public static bool CommandFormatValid(string commandtext)
        {
            if (!string.IsNullOrEmpty(commandtext))
            {
                string allowableLetters = "LRF";

                foreach (char c in commandtext)
                {
                    // This is using String.Contains for .NET 2 compat.,
                    //   hence the requirement for ToString()
                    if (!allowableLetters.Contains(c.ToString()))
                        return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        //validate width and height with current position
        public static bool validateWidthAndHeight(int? width, int? height, string carPosition)
        {
            try
            {
                carPosition = carPosition.Substring(1, (carPosition.Length - 2)); // removing brackets and validating
                var axis = carPosition.Split(',');
                //axis[0] means x axis and axis[1] means y axis
                if (width > Convert.ToInt32(axis[0]) && height > Convert.ToInt32(axis[1]))
                {
                    if (Convert.ToInt32(axis[0]) < 0 && Convert.ToInt32(axis[1]) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return false;
            }
        }

        //Method to calculate Forward Position
        public static string calculateForwardPosition(string facingDirection, int xCoordinate, int yCoordinate, string isCurrentAxis)
        {
            try
            {
                if (facingDirection.ToString() == "S" && isCurrentAxis == "y")
                {
                    if (!((yCoordinate - 1) < 0))
                    {
                        yCoordinate = yCoordinate - 1;
                    }
                }
                else if (facingDirection.ToString() == "N" && isCurrentAxis == "y")
                {
                    yCoordinate = yCoordinate + 1;
                }
                else if (facingDirection.ToString() == "E" && isCurrentAxis == "x")
                {
                    xCoordinate = xCoordinate + 1;
                }
                else if (facingDirection.ToString() == "W" && isCurrentAxis == "x")
                {
                    if (!((xCoordinate - 1) < 0))
                    {
                        xCoordinate = xCoordinate - 1;
                    }
                }
                return "(" + xCoordinate + "," + yCoordinate + ")";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return null;
            }
        }

        //Method to find the Left facing direction based on current facing direction
        public static string findLeftFacingDirection(string facingDirection)
        {
            if (facingDirection == "N")
            {
                facingDirection = "W";
            }
            else if (facingDirection == "S")
            {
                facingDirection = "E";
            }
            else if (facingDirection == "E")
            {
                facingDirection = "N";
            }
            else if (facingDirection == "W")
            {
                facingDirection = "S";
            }
            return facingDirection;
        }

        //Method to find the Right facing direction based on current facing direction
        public static string findRightFacingDirection(string facingDirection)
        {
            if (facingDirection == "N")
            {
                facingDirection = "E";
            }
            else if (facingDirection == "S")
            {
                facingDirection = "W";
            }
            else if (facingDirection == "E")
            {
                facingDirection = "S";
            }
            else if (facingDirection == "W")
            {
                facingDirection = "N";
            }
            return facingDirection;
        }

        //Method to execute the commands for auto drive and get the car position and direction as the result 
        public static CarSimulationInput ExecuteCarAutoDriveCommands(CarSimulationInput simulationInput)
        {
            try
            {
                bool isValidWidthAndHeight = false;
                string currentFacingDirection = simulationInput.FacingDirection.ToString();
                var command = simulationInput.Commands;
                var isCurrentAxis = currentFacingDirection == Constants.North || currentFacingDirection == Constants.South ? Constants.yaxis : Constants.xaxis;

                while (command != null && command != "")
                {
                    string firststr = command.Substring(0, 1);
                    string trimmedCommand = command.Remove(0, 1);

                    string val = simulationInput.CurrentPosition.Replace("(", "").Replace(")", "");
                    var x = Convert.ToInt32(val.Split(",")[0]);
                    var y = Convert.ToInt32(val.Split(",")[1]);


                    //for forward command
                    if (firststr == Constants.Forward)
                    {
                        var calculatedPosition = CarSimulationService.calculateForwardPosition(currentFacingDirection, x, y, isCurrentAxis);

                        //validating the x and y axis values and exceeds the boundary so skipping the command
                        isValidWidthAndHeight = CarSimulationService.validateWidthAndHeight(simulationInput.Width, simulationInput.Height, calculatedPosition);
                        if (isValidWidthAndHeight)
                        {
                            simulationInput.CurrentPosition = calculatedPosition;
                        }
                        command = trimmedCommand;
                    }
                    else if (firststr == Constants.Right) //for right command
                    {
                        currentFacingDirection = CarSimulationService.findRightFacingDirection(currentFacingDirection);
                        isCurrentAxis = currentFacingDirection == Constants.North || currentFacingDirection == Constants.South ? Constants.yaxis : Constants.xaxis;
                        command = trimmedCommand;
                    }
                    else if (firststr == Constants.Left) //for left command
                    {
                        currentFacingDirection = CarSimulationService.findLeftFacingDirection(currentFacingDirection);
                        isCurrentAxis = currentFacingDirection == Constants.North || currentFacingDirection == Constants.South ? Constants.yaxis : Constants.xaxis;
                        command = trimmedCommand;
                    }
                }
                simulationInput.Positionresult = simulationInput.CurrentPosition;
                simulationInput.Directionresult = currentFacingDirection;
                return simulationInput;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
