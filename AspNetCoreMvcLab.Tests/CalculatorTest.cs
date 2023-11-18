namespace AspNetCoreMvcLab.Tests
{
    public class CalculatorTest
    {
        [Fact]
        public void TestingAddition()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act 
            int result = calculator.Add(2, 3);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void TestingDivisionWithNonZeroNumbers()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act 
            int result = calculator.Divide(20, 10);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestingDivisionWithZeroNumbers()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act 
            int result = calculator.Divide(10, 0);

            // Assert
            Assert.Equal(0, result);
        }

    }

    public class Calculator
    {
        public int Add(int x, int y)
        {
            return x - y;
        }

        public int Divide(int x, int y)
        {
            if (y == 0)
            {
                return 0;
            }

            return x / y;
        }
    }
}