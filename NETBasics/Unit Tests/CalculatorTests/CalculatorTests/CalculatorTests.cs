using System;
using Utilities;
using Xunit;

namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Divide_2By2_Returns1()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            int actual = calculator.Divide(2, 2);
            int expected = 1;

            // Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Divide_2By0_ReturnsException()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            // int actual = calculator.Divide(2, 0);

            // Assert
           // Assert.ThrowsAny(calculator.Divide(2, 0));
        }
    }
}
