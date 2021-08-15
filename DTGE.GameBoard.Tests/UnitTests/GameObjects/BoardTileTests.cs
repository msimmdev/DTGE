using System;
using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;
using DTGE.Common.Base;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class BoardTileTests
    {
        private readonly BoardTile sut;
        public BoardTileTests()
        {
            sut = new BoardTile();
        }

        [Fact]
        public void Constructor_Empty_ShouldSetNonEmptyGuidId()
        {
            Assert.IsType<Guid>(sut.Id);
            Assert.False(sut.Id == new Guid());
        }

        [Fact]
        public void Constructor_Empty_ShouldSetEmputTags()
        {
            Assert.Empty(sut.Tags);
        }

        [Fact]
        public void Constructor_Empty_ShouldSetEmputState()
        {
            Assert.IsType<EmptyState>(sut.State);
        }


        [Fact]
        public void Board_GetAndSet_ShouldMatch()
        {
            var board = new Mock<IBoard>();

            sut.Board = board.Object;
            var getBoard = sut.Board;

            Assert.Equal(board.Object, getBoard);
        }

        [Fact]
        public void Position_GetAndSet_ShouldMatch()
        {
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(true);

            sut.Position = pos.Object;
            var getPos = sut.Position;

            Assert.Equal(pos.Object, getPos);
        }

        [Fact]
        public void GetDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var board = new Mock<IBoard>();
            board.SetupGet(x => x.Id).Returns(someGuid);
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(true);

            sut.Board = board.Object;
            sut.Position = pos.Object;
            var data = sut.GetDto();
            var boardTileData = data as BoardTileDto;

            Assert.Equal(sut.Id.ToString(), boardTileData.Id);
            Assert.Equal(someGuid.ToString(), boardTileData.BoardId);
        }

        [Fact]
        public void GetDto_IncompleteData_ShouldMatchData()
        {
            var data = sut.GetDto();
            var boardTileData = data as BoardTileDto;

            Assert.Equal(sut.Id.ToString(), boardTileData.Id);
            Assert.Null(boardTileData.BoardId);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someGuid2 = Guid.NewGuid();
            var mockPosition = new Mock<IBoardPosition>();
            mockPosition.SetupAllProperties();
            var posDto = new BoardPositionDto()
            {
                Position = "3x7"
            };
            var data = new BoardTileDto()
            {
                Id = someGuid.ToString(),
                BoardId = someGuid2.ToString(),
                Position = posDto
            };

            var mockResolver = new Mock<IResolver>();
            mockResolver.Setup(x => x.Create<IBoardPosition>(posDto)).Returns(mockPosition.Object);

            sut.UseDto(data, mockResolver.Object);

            Assert.Equal(someGuid, sut.Id);
            Assert.Same(mockPosition.Object, sut.Position);
        }

        [Fact]
        public void UseDto_IncompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someGuid2 = Guid.NewGuid();
            var data = new BoardTileDto()
            {
                Id = someGuid.ToString(),
                BoardId = someGuid2.ToString(),
            };

            var mockResolver = new Mock<IResolver>();

            sut.UseDto(data, mockResolver.Object);

            Assert.Equal(someGuid, sut.Id);
            Assert.Null(sut.Position);
        }

        [Fact]
        public void Equals_OtherTile_ShouldReturnFalse()
        {
            var otherSut = new BoardTile();

            var result = sut.Equals(otherSut as IdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherTileInterface_ShouldReturnFalse()
        {
            var otherSut = new BoardTile();

            var result = sut.Equals(otherSut as IIdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherObject_ShouldReturnFalse()
        {
            var otherSut = new BoardTile();

            var result = sut.Equals(otherSut as Object);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_OtherObject_ShouldNotMatch()
        {
            var otherSut = new BoardTile();

            var result = sut.GetHashCode() == otherSut.GetHashCode();

            Assert.False(result);
        }
    }
}
