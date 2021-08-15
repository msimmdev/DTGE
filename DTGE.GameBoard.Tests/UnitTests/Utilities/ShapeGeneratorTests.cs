using System.Linq;
using Xunit;
using DTGE.GameBoard.Utilities;

namespace DTGE.GameBoard.Tests.UnitTests.Utilities
{
    public class ShapeGeneratorTests
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 10, 10)]
        [InlineData(10, 1, 10)]
        [InlineData(10, 10, 100)]
        [InlineData(3, 5, 15)]
        public void QuadRectangle_Theory_ShouldReturnNumberPos(int x, int y, int res)
        {
            var sut = ShapeGenerator.GenerateRectangleShapeWithQuads(x, y);

            Assert.Equal(res, sut.Positions.Count());
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 10, 10)]
        [InlineData(10, 1, 10)]
        [InlineData(10, 10, 36)]
        [InlineData(3, 5, 12)]
        public void QuadRectanglePerimeter_Theory_ShouldReturnNumberPos(int x, int y, int res)
        {
            var sut = ShapeGenerator.GeneratePerimeterShapeWithQuads(x, y);

            Assert.Equal(res, sut.Positions.Count());
        }
    }
}
