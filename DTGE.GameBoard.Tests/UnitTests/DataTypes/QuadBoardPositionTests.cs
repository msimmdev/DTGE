using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.DataTypes;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.DataTypes
{
    public class QuadBoardPositionTests
    {
        [Fact]
        public void Constructor_CreateAndGet_ShouldMatch()
        {
            var sut = new QuadBoardPosition(3,7);

            Assert.Equal(3, sut.X);
            Assert.Equal(7, sut.Y);
        }

        [Fact]
        public void Equals_MatchingValues_ShouldReturnTrue()
        {
            var sut1 = new QuadBoardPosition(3, 7);
            var sut2 = new QuadBoardPosition(3, 7);

            Assert.True(sut1.Equals(sut2));
            Assert.True(sut2.Equals(sut1));
        }

        [Theory]
        [InlineData(3, 8)]
        [InlineData(4, 7)]
        [InlineData(5, 1)]
        [InlineData(1, 1)]
        public void Equals_NonMatchingValues_ShouldReturnFalse(int x, int y)
        {
            var sut1 = new QuadBoardPosition(3, 7);
            var sut2 = new QuadBoardPosition(x, y);

            Assert.False(sut1.Equals(sut2));
            Assert.False(sut2.Equals(sut1));
        }

        [Fact]
        public void GetDto_CompleteData_ShouldMatchData()
        {
            var sut = new QuadBoardPosition(3, 7);

            var data = sut.GetDto() as BoardPositionDto;

            Assert.Equal("3x7", data.Position);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var sut = new QuadBoardPosition(3, 7);
            var data = new BoardPositionDto()
            {
                Position="4x9"
            };

            sut.UseDto(data, null);

            Assert.Equal(4, sut.X);
            Assert.Equal(9, sut.Y);
        }
    }
}
