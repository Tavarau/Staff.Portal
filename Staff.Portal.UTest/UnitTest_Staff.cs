using Moq;
using Staff.Portal.BusinessAccess;

namespace Staff.Portal.UTest
{
    public class UnitTest_Staff
    {
        [Theory]
        [InlineData(5, 2, 20000)]
        [InlineData(6, 4, 48000)]
        [InlineData(7, 6, 84000)]
        [InlineData(5, 8, 80000)]
        [InlineData(8, 8, 128000)]
        [InlineData(5, 10, 100000)]
        [InlineData(7, 15, 210000)]
        public void WhenCalculateSalary(int Level, int YearOfExperience, double ExpectedValue)
        {

            //Arrange

            var mockRepo = new Mock<ISalaryCalculator>();
            mockRepo.Setup(x => x.CalculateSalary(It.IsAny<int>(), It.IsAny<int>())).Returns(() => ExpectedValue);

            //Act
            double Actual = mockRepo.Object.CalculateSalary(Level, YearOfExperience);


            //Assert

            Assert.Equal(ExpectedValue, Actual);
        }
    }
}