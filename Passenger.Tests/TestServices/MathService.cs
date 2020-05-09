namespace Passenger.Tests.TestServices
{
    public class MathService : IMathService
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
