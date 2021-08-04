using Xunit;
using Moq;
using DTGE.GameBoard.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Tests.UnitTests.GameActions
{
    public class MoveTileActionTests
    {
        private Mock<IBoardTile> mockTile;
        private Mock<IBoardPosition> mockPosition;
        private MoveTileAction sut;
        public MoveTileActionTests()
        {
            mockTile = new Mock<IBoardTile>();
            mockTile.Setup(x => x.Equals(mockTile.Object)).Returns(true);
            mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);
            sut = new MoveTileAction(mockTile.Object, mockPosition.Object);
        }

        [Fact]
        public void Constructor_GetTileAndPosition_ShouldReturnMatchingValues()
        {
            var tile = sut.Tile;
            var pos = sut.NewPosition;

            Assert.Equal(mockTile.Object, tile);
            Assert.Equal(mockPosition.Object, pos);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(false);
            mockTile.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_WithOrphanedTile_ShouldReturnError()
        {

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithOccpiedPosition_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(true);
            mockTile.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }
    }
}
