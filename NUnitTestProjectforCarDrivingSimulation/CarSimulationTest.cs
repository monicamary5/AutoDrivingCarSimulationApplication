using AutoDrivingCarSimulationApplication.Models;
using NUnit.Framework;

namespace NUnitTestProjectforCarDrivingSimulation
{
    [TestFixture]
    public class CarSimulationTest
    {
        [Test]
        public void AdditionOfTwoNumber_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            CarSimulationInput simulation = new();

            //Act
            int result = simulation.AdditionOfTwoNumber(10, 20);

            //Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        public void AdditionOfTwoNumber_InputTwoInt_GetWrongAddition()
        {
            //Arrange
            ArithmeticOperation calc = new();

            //Act
            int result = calc.AdditionOfTwoNumber(10, 20);

            //Assert
            Assert.AreEqual(40, result);
        }

    }
}