namespace Nunit.Demo
{
    public class AutoCarDrivingSimulation
    {
        public int ValidateWidthAndHeight(int first, int second)
        {
            return first + second;
        }

        public int ValidateCarPosition(int first, int second)
        {
            if (first < second)
                throw new ArgumentException($"First number {first} is less than second number {second}");

            return first - second;
        }

        public int ValidateExecuteCommand(int first, int second)
        {
            if (first < second)
                throw new ArgumentException($"First number {first} is less than second number {second}");

            return first - second;
        }

    }
}