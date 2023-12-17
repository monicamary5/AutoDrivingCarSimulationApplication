using AutoDrivingCarSimulationApplication.Helpers;
using AutoDrivingCarSimulationApplication.Models;
using AutoDrivingCarSimulationApplication.Service;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;

namespace Nunit.Demo.Test
{
    [TestFixture]
    public class AutoCarDrivingSimulationTest
    {
        [Test]
        public void Test_ValidCarCurrentPosition()
        {
            var simInput = new CarSimulationInput();
            simInput.CurrentPosition = "(1, 2)";
            var result = CarSimulationService.validateCurrentPosition(simInput.CurrentPosition);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Test_InvalidCarCurrentPosition()
        {
            var simInput = new CarSimulationInput();
            simInput.CurrentPosition = "(1, 2, 45)";
            var result = CarSimulationService.validateCurrentPosition(simInput.CurrentPosition);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Test_ValidExecuteCommandFormat()
        {
            var simInput = new CarSimulationInput();
            simInput.Commands = "FFRRLLFF";
            var result = CarSimulationService.CommandFormatValid(simInput.Commands);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Test_invalidExecuteCommandFormat()
        {
            var simInput = new CarSimulationInput();
            simInput.Commands = "FFMMXXTT";
            var result = CarSimulationService.CommandFormatValid(simInput.Commands);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Test_ValidWidthAndHeight()
        {
            var simInput = new CarSimulationInput();
            simInput.Width = 10;
            simInput.Height = 10;
            simInput.CurrentPosition = "(1, 4)";
            var result = CarSimulationService.validateWidthAndHeight(simInput.Width, simInput.Height,simInput.CurrentPosition);
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void Test_invalidWidth()
        {
            var simInput = new CarSimulationInput();
            simInput.Width = 4;
            simInput.Height = 10;
            simInput.CurrentPosition = "(5, 6)";
            var result = CarSimulationService.validateWidthAndHeight(simInput.Width, simInput.Height, simInput.CurrentPosition);
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Test_invalidHeight()
        {
            var simInput = new CarSimulationInput();
            simInput.Width = 4;
            simInput.Height = 5;
            simInput.CurrentPosition = "(3, 6)";
            var result = CarSimulationService.validateWidthAndHeight(simInput.Width, simInput.Height, simInput.CurrentPosition);
            ClassicAssert.IsFalse(result);
        }

        public void Test_AutoDriveSimulationPositionResultOutput()
        {
            var simInput = new CarSimulationInput();
            simInput.Width = 10;
            simInput.Height = 10;
            simInput.CurrentPosition = "(1, 2)";
            simInput.FacingDirection = Direction.N;
            simInput.Commands = "FFRFFFRRLF";
            var result = CarSimulationService.ExecuteCarAutoDriveCommands(simInput);
            ClassicAssert.AreEqual(result.Positionresult, (4,3));
        }

        public void Test_AutoDriveSimulationDirectionResultOutput()
        {
            var simInput = new CarSimulationInput();
            simInput.Width = 10;
            simInput.Height = 10;
            simInput.CurrentPosition = "(1, 2)";
            simInput.FacingDirection = Direction.N;
            simInput.Commands = "FFRFFFRRLF";
            var result = CarSimulationService.ExecuteCarAutoDriveCommands(simInput);
            ClassicAssert.AreEqual(result.Directionresult, Constants.South);
        }
    }
}