using Xunit;
using Moq;
using DTGE.GameBoard.GameEvents;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Tests.UnitTests.GameEvents
{
    public class ObjectMovesToTileEventTests
    {
        [Fact]
        public void Constructor_WithValidData_CanGetData()
        {
            var mockBoardObject = new Mock<IBoardObject>();
            var mockTile = new Mock<IBoardTile>();

            var sut = new ObjectMovesToTileEvent(mockBoardObject.Object, mockTile.Object);

            Assert.Same(mockBoardObject.Object, sut.Object);
            Assert.Same(mockTile.Object, sut.Tile);
        }
    }
}
