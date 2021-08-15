using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;

namespace DTGE.Common.Tests.Core
{
    public class EmptyStateTests
    {
        private EmptyState sut;
        public EmptyStateTests()
        {
            sut = new EmptyState();
        }

        [Fact]
        public void Equals_OtherEmpty_ShouldReturnTrue()
        {
            var otherSut = new EmptyState();

            var result = sut.Equals(otherSut);

            Assert.True(result);
        }

        [Fact]
        public void Equals_OtherEmptyInterface_ShouldReturnTrue()
        {
            var otherSut = new EmptyState();

            var result = sut.Equals(otherSut as IGameState);

            Assert.True(result);
        }

        [Fact]
        public void GetDto_Blank_ShouldReturnEmptyDto()
        {
            var result = sut.GetDto();

            Assert.IsType<EmptyDto>(result);
        }

        [Fact]
        public void UseDto_Blank_ShouldNotThrowException()
        {
            var dto = new EmptyDto();
            var mockResolver = new Mock<IResolver>();

            sut.UseDto(dto, mockResolver.Object);
        }
    }
}
