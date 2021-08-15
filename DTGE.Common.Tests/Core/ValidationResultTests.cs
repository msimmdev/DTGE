using Xunit;
using Moq;
using DTGE.Common.Core;

namespace DTGE.Common.Tests.Core
{
    public class ValidationResultTests
    {
        [Fact]
        public void Constructor_Success_ShouldCreate()
        {
            var sut = new ValidationResult(true, "");

            Assert.IsType<ValidationResult>(sut);
            Assert.True(sut.Success);
        }

        [Fact]
        public void Constructor_Error_ShouldCreate()
        {
            var sut = new ValidationResult(false, It.IsAny<string>());

            Assert.IsType<ValidationResult>(sut);
            Assert.False(sut.Success);
        }

        [Fact]
        public void NewSuccess_Blank_ShouldCreateSuccess()
        {
            var sut = ValidationResult.NewSuccess();

            Assert.IsType<ValidationResult>(sut);
            Assert.True(sut.Success);
        }

        [Fact]
        public void NewSuccess_Blank_ShouldCreateError()
        {
            var sut = ValidationResult.NewError(It.IsAny<string>());

            Assert.IsType<ValidationResult>(sut);
            Assert.False(sut.Success);
        }
    }
}
