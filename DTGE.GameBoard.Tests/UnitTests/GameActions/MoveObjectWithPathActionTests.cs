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
using DTGE.GameBoard.GameEvents;

namespace DTGE.GameBoard.Tests.UnitTests.GameActions
{
    public class MoveObjectWithPathActionTests
    {
        private Mock<IBoardObject> mockObject;
        private Mock<ITilePath> mockPath;
        private MoveObjectWithPathAction sut;

        public MoveObjectWithPathActionTests()
        {
            mockObject = new Mock<IBoardObject>();
            mockPath = new Mock<ITilePath>();
            sut = new MoveObjectWithPathAction(mockObject.Object, mockPath.Object);
        }

        [Fact]
        public void Constructor_GetTile_ShouldReturnMatchingValue()
        {
            var boardObject = sut.Object;

            Assert.Same(mockObject.Object, boardObject);
        }

        [Fact]
        public void Constructor_GetPath_ShouldReturnMatchingValue()
        {
            var path = sut.Path;

            Assert.Same(mockPath.Object, path);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockObject.SetupGet(x => x.Position).Returns(mockPosition.Object);

            var result = sut.Validate();

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_WithDetachedObject_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupGet(x => x.Position).Returns(mockPosition.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithNoPositionObject_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithOneTile_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockObject.SetupGet(x => x.Position).Returns(mockPosition.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithNullTile_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockObject.SetupGet(x => x.Position).Returns(mockPosition.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithInvalidStartingPoint_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition2.Object)).Returns(true);
            mockPosition.Setup(x => x.Equals(mockPosition2.Object)).Returns(false);
            mockPosition2.Setup(x => x.Equals(mockPosition.Object)).Returns(false);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockObject.SetupGet(x => x.Position).Returns(mockPosition2.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Execute_WithValidData_ShouldChangeTilePosition()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition2.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockTile2.SetupGet(x => x.Position).Returns(mockPosition2.Object);
            mockTile2.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupProperty(x => x.Board);
            mockObject.SetupProperty(x => x.Position);

            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            Assert.Same(mockPosition2.Object, sut.Object.Position);
            Assert.Same(mockBoard.Object, sut.Object.Board);
        }

        [Fact]
        public void Execute_WithValidData_ShouldTriggerEvents()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.Equals(mockBoard.Object)).Returns(true);

            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition2.Object)).Returns(true);

            var mockTile1 = new Mock<IBoardTile>();
            mockTile1.Setup(x => x.Equals(mockTile1.Object)).Returns(true);
            var mockTile2 = new Mock<IBoardTile>();
            mockTile2.Setup(x => x.Equals(mockTile2.Object)).Returns(true);
            var tileList = new List<IBoardTile>() { mockTile1.Object, mockTile2.Object };

            mockTile1.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockTile1.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockTile2.SetupGet(x => x.Position).Returns(mockPosition2.Object);
            mockTile2.SetupGet(x => x.Board).Returns(mockBoard.Object);

            mockPath.SetupGet(x => x.Tiles).Returns(tileList);
            mockObject.SetupProperty(x => x.Board);
            mockObject.SetupProperty(x => x.Position);

            var mockEventHandler = new Mock<IEventHandler>();

            sut.Execute(mockEventHandler.Object);

            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ActionStartEvent<MoveObjectWithPathAction>>()), Times.Once);
            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ActionEndEvent<MoveObjectWithPathAction>>()), Times.Once);
            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ObjectMovesToTileEvent>()), Times.Once);
            mockEventHandler.Verify(x => x.Dispatch(It.IsAny<ObjectTraversesTileEvent>()), Times.Once);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var objectGuid = Guid.NewGuid();
            var pathGuid = Guid.NewGuid();
            var tileId = Guid.NewGuid();
            var pathDto = new TilePathDto()
            {
                Id = pathGuid.ToString(),
                Distance = 1,
                TileIds = new List<string>() { tileId.ToString() },
            };

            var tags = new HashSet<string>() { "tag1", "tag2" };

            mockObject.SetupGet(x => x.Id).Returns(objectGuid);
            mockPath.Setup(x => x.GetDto()).Returns(pathDto);

            sut.Tags = tags;

            var result = sut.GetDto() as MoveObjectWithPathActionDto;

            Assert.Equal(sut.Id.ToString(), result.Id);
            Assert.Equal(tags, result.Tags);
            Assert.Equal(objectGuid.ToString(), result.ObjectId);
            Assert.Equal(pathDto, result.Path);
        }

        [Fact]
        public void GetDto_NullData_ShouldReturnValidDto()
        {
            mockObject.SetupGet(x => x.Id).Returns(null);

            var result = sut.GetDto() as MoveObjectWithPathActionDto;

            Assert.Equal(sut.Id.ToString(), result.Id);
            Assert.Equal(Guid.Empty.ToString(), result.ObjectId);
            Assert.Null(result.Path);

        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var objectGuid = Guid.NewGuid();
            var pathGuid = Guid.NewGuid();
            var tileId = Guid.NewGuid();
            var pathDto = new TilePathDto()
            {
                Id = pathGuid.ToString(),
                Distance = 1,
                TileIds = new List<string>() { tileId.ToString() },
            };
            var tags = new List<string>() { "tag1", "tag2" };

            var dto = new MoveObjectWithPathActionDto()
            {
                Id = idGuid.ToString(),
                ObjectId = objectGuid.ToString(),
                Path = pathDto,
                Tags = tags
            };

            var mockResolver = new Mock<IResolver>();
            mockResolver.Setup(x => x.Resolve<IBoardObject>(It.IsAny<Guid>())).Returns(mockObject.Object);
            mockResolver.Setup(x => x.Create<ITilePath>(pathDto)).Returns(mockPath.Object);

            sut.UseDto(dto, mockResolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(tags, sut.Tags);
            Assert.Same(mockObject.Object, sut.Object);
            Assert.Same(mockPath.Object, sut.Path);
        }

        [Fact]
        public void Equals_OtherAction_ShouldReturnFalse()
        {
            var otherSut = new MoveObjectWithPathAction(mockObject.Object, mockPath.Object);

            var result = sut.Equals(otherSut as IdentifiedAction);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherActionInterface_ShouldReturnFalse()
        {
            var otherSut = new MoveObjectWithPathAction(mockObject.Object, mockPath.Object);

            var result = sut.Equals(otherSut as IIdentifiedAction);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherObject_ShouldReturnFalse()
        {
            var otherSut = new MoveObjectWithPathAction(mockObject.Object, mockPath.Object);

            var result = sut.Equals(otherSut as Object);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_OtherObject_ShouldNotMatch()
        {
            var otherSut = new MoveObjectWithPathAction(mockObject.Object, mockPath.Object);

            var result = sut.GetHashCode() == otherSut.GetHashCode();

            Assert.False(result);
        }
    }
}
