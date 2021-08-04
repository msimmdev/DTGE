using System;
using System.Linq;
using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.GameBoard.DataTypes;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class BoardObjectTests
    {
        private BoardObject sut;
        public BoardObjectTests()
        {
            sut = new BoardObject();
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
        public void Tags_CardAddAndGet_ShouldReturnSingleList()
        {
            var testString = "ONE";

            sut.Tags.Add(testString);
            var tag = sut.Tags.First();

            Assert.Equal(testString, tag);
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
            var boardObjectData = data as BoardObjectDto;

            Assert.Equal(sut.Id.ToString(), boardObjectData.Id);
            Assert.Equal(someGuid.ToString(), boardObjectData.BoardId);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someGuid2 = Guid.NewGuid();
            var data = new BoardObjectDto()
            {
                Id = someGuid.ToString(),
                BoardId = someGuid2.ToString(),
                Position = new BoardPositionDto()
                {
                    Position = "3x7"
                }
            };

            sut.UseDto(data, null);
            var pos = sut.Position as QuadBoardPosition;

            Assert.Equal(someGuid, sut.Id);
            Assert.Equal(3, pos.X);
            Assert.Equal(7, pos.Y);
        }
    }
}
