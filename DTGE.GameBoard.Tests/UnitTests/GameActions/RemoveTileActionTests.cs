using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.Common.Interfaces;
using DTGE.Common.Core;
using DTGE.Common.Base;
using DTGE.GameBoard.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.GameActions
{
    public class RemoveTileActionTests
    {
        private Mock<IBoardTile> mockTile;
        private RemoveTileAction sut;

        public RemoveTileActionTests()
        {
            mockTile = new Mock<IBoardTile>();
            sut = new RemoveTileAction(mockTile.Object);
        }

        [Fact]
        public void Constructor_GetTile_ShouldReturnMatchingValue()
        {
            var tile = sut.Tile;

            Assert.Same(mockTile.Object, tile);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            var mockBoard = new Mock<IBoard>();
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
        public void Execute_WithValidData_ShouldDetechTile()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(x => x.Tiles).Returns(new Dictionary<Guid, IBoardTile>());
            mockTile.SetupProperty(x => x.Board);
            mockTile.Object.Board = mockBoard.Object;
            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            Assert.Null(sut.Tile.Board);
        }

        [Fact]
        public void Execute_WithValidData_ShouldRemovePosition()
        {
            var mockPosition = new Mock<IBoardPosition>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(x => x.Tiles).Returns(new Dictionary<Guid, IBoardTile>());
            mockTile.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockTile.SetupProperty(x => x.Position);
            mockTile.Object.Position = mockPosition.Object;
            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            Assert.Null(sut.Tile.Position);
        }

        [Fact]
        public void Execute_WithValidData_ShouldTriggerEvents()
        {
            var mockPosition = new Mock<IBoardPosition>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(x => x.Tiles).Returns(new Dictionary<Guid, IBoardTile>());
            mockTile.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockTile.SetupProperty(x => x.Position);
            mockTile.Object.Position = mockPosition.Object;
            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ActionStartEvent<RemoveTileAction>>()), Times.Once);
            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ActionEndEvent<RemoveTileAction>>()), Times.Once);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var tileGuid = Guid.NewGuid();
            mockTile.SetupGet(x => x.Id).Returns(tileGuid);

            var result = sut.GetDto();
            var dto = result as RemoveTileActionDto;

            Assert.Equal(sut.Id.ToString(), dto.Id);
            Assert.Equal(tileGuid.ToString(), dto.TileId);
        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var tileGuid = Guid.NewGuid();

            mockTile.SetupGet(x => x.Id).Returns(tileGuid);

            var resolver = new Mock<IResolver>();
            resolver.Setup(x => x.Resolve<IBoardTile>(It.IsAny<Guid>())).Returns(mockTile.Object);

            var dto = new RemoveTileActionDto()
            {
                Id = idGuid.ToString(),
                TileId = tileGuid.ToString(),
                Tags = new List<string>()
            };

            sut.UseDto(dto, resolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(tileGuid, sut.Tile.Id);
        }

        [Fact]
        public void Equals_OtherAction_ShouldReturnFalse()
        {
            var otherSut = new RemoveTileAction(mockTile.Object);

            var result = sut.Equals(otherSut as IdentifiedAction);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherActionInterface_ShouldReturnFalse()
        {
            var otherSut = new RemoveTileAction(mockTile.Object);

            var result = sut.Equals(otherSut as IIdentifiedAction);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherObject_ShouldReturnFalse()
        {
            var otherSut = new RemoveTileAction(mockTile.Object);

            var result = sut.Equals(otherSut as Object);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_OtherObject_ShouldNotMatch()
        {
            var otherSut = new RemoveTileAction(mockTile.Object);

            var result = sut.GetHashCode() == otherSut.GetHashCode();

            Assert.False(result);
        }
    }
}
