using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.Tests.UnitTests.GameActions
{
    public class PlaceObjectOnBoardActionTests
    {
        private Mock<IBoard> mockBoard;
        private Mock<IBoardObject> mockObject;
        private Mock<IBoardPosition> mockPosition;
        private PlaceObjectOnBoardAction sut;

        public PlaceObjectOnBoardActionTests()
        {
            mockBoard = new Mock<IBoard>();
            mockObject = new Mock<IBoardObject>();
            mockPosition = new Mock<IBoardPosition>();
            sut = new PlaceObjectOnBoardAction(mockBoard.Object, mockObject.Object, mockPosition.Object);
        }

        [Fact]
        public void Constructor_GetTile_ShouldReturnMatchingValue()
        {
            var boardObject = sut.Object;

            Assert.Same(mockObject.Object, boardObject);
        }

        [Fact]
        public void Constructor_GetPosition_ShouldReturnMatchingValue()
        {
            var pos = sut.Position;

            Assert.Same(mockPosition.Object, pos);
        }

        [Fact]
        public void Constructor_GetBoard_ShouldReturnMatchingValue()
        {
            var pos = sut.Board;

            Assert.Same(mockBoard.Object, pos);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(false);

            var result = sut.Validate();

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_WithAttachedObject_ShouldReturnError()
        {
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(false);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithPosition_ShouldReturnError()
        {
            mockObject.SetupGet(x => x.Position).Returns(mockPosition.Object);
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(false);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithOccpiedPosition_ShouldReturnError()
        {
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(true);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Execute_WithValidData_ShouldSetPosition()
        {
            mockObject.SetupProperty(x => x.Position);
            mockBoard.SetupGet(x => x.Objects).Returns(new Dictionary<Guid, IBoardObject>());

            sut.Execute();

            Assert.Same(mockPosition.Object, sut.Object.Position);
        }

        [Fact]
        public void Execute_WithValidData_ShouldAttachTileToBoard()
        {
            mockObject.SetupProperty(x => x.Board);
            mockBoard.SetupGet(x => x.Objects).Returns(new Dictionary<Guid, IBoardObject>());

            sut.Execute();

            Assert.Same(mockBoard.Object, sut.Object.Board);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var objectGuid = Guid.NewGuid();
            var boardGuid = Guid.NewGuid();
            var boardPositionDto = new BoardPositionDto() { Position = "3x4" };
            mockObject.SetupGet(x => x.Id).Returns(objectGuid);
            mockBoard.SetupGet(x => x.Id).Returns(boardGuid);
            mockPosition.Setup(x => x.GetDto()).Returns(boardPositionDto);

            var result = sut.GetDto();
            var dto = result as PlaceObjectOnBoardActionDto;

            Assert.Equal(boardPositionDto.Position, dto.Position.Position);
            Assert.Equal(sut.Id.ToString(), dto.Id);
            Assert.Equal(objectGuid.ToString(), dto.ObjectId);
            Assert.Equal(boardGuid.ToString(), dto.BoardId);
        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var objectGuid = Guid.NewGuid();
            var boardGuid = Guid.NewGuid();
            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            mockObject.SetupGet(x => x.Id).Returns(objectGuid);
            mockBoard.SetupGet(x => x.Id).Returns(boardGuid);

            var resolver = new Mock<IObjectResolver>();
            resolver.Setup(x => x.Resolve<IBoardObject>(It.IsAny<Guid>())).Returns(mockObject.Object);
            resolver.Setup(x => x.Resolve<IBoard>(It.IsAny<Guid>())).Returns(mockBoard.Object);
            resolver.Setup(x => x.Create<IBoardPosition>(It.IsAny<BoardPositionDto>())).Returns(mockPosition2.Object);

            var dto = new PlaceObjectOnBoardActionDto()
            {
                Id = idGuid.ToString(),
                ObjectId = objectGuid.ToString(),
                BoardId = boardGuid.ToString(),
                Position = new BoardPositionDto(),
                Tags = new List<string>()
            };

            sut.UseDto(dto, resolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(objectGuid, sut.Object.Id);
            Assert.Equal(boardGuid, sut.Board.Id);
            Assert.Same(mockPosition2.Object, sut.Position);
        }
    }
}
