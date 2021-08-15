using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.DataTypes;
using DTGE.GameBoard.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.Common.Interfaces;

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
        public void Constructor_GetTile_ShouldReturnMatchingValue()
        {
            var tile = sut.Tile;

            Assert.Same(mockTile.Object, tile);
        }

        [Fact]
        public void Constructor_GetPosition_ShouldReturnMatchingValue()
        {
            var pos = sut.NewPosition;

            Assert.Same(mockPosition.Object, pos);
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

        [Fact]
        public void Execute_WithValidData_ShouldChangeTilePosition()
        {
            mockTile.SetupProperty(x => x.Position);
            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            Assert.Same(mockPosition.Object, sut.Tile.Position);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var tileGuid = Guid.NewGuid();
            var boardPositionDto = new BoardPositionDto() { Position = "3x4" };
            mockTile.SetupGet(x => x.Id).Returns(tileGuid);
            mockPosition.Setup(x => x.GetDto()).Returns(boardPositionDto);

            var result = sut.GetDto();
            var dto = result as MoveTileActionDto;

            Assert.Equal(boardPositionDto.Position, dto.NewPosition.Position);
            Assert.Equal(sut.Id.ToString(), dto.Id);
            Assert.Equal(tileGuid.ToString(), dto.TileId);
        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var tileGuid = Guid.NewGuid();
            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            mockTile.SetupGet(x => x.Id).Returns(tileGuid);

            var resolver = new Mock<IResolver>();
            resolver.Setup(x => x.Resolve<IBoardTile>(It.IsAny<Guid>())).Returns(mockTile.Object);
            resolver.Setup(x => x.Create<IBoardPosition>(It.IsAny<BoardPositionDto>())).Returns(mockPosition2.Object);

            var dto = new MoveTileActionDto()
            {
                Id = idGuid.ToString(),
                TileId = tileGuid.ToString(),
                NewPosition = new BoardPositionDto(),
                Tags = new List<string>()
            };

            sut.UseDto(dto, resolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(tileGuid, sut.Tile.Id);
            Assert.Same(mockPosition2.Object, sut.NewPosition);
        }
    }
}
