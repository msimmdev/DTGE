using System;
using Xunit;
using Moq;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.GameBoard.DataTypes;

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
            Assert.False(sut.Id == new Guid());
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
        public void GetSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var board = new Mock<IBoard>();
            board.SetupGet(x => x.Id).Returns(someGuid);
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(true);

            sut.Board = board.Object;
            sut.Position = pos.Object;
            var data = sut.GetSerializationData();
            var boardTileData = data as BoardTileSerializationData;

            Assert.Equal(sut.Id.ToString(), boardTileData.Id);
            Assert.Equal(someGuid.ToString(), boardTileData.BoardId);
        }

        [Fact]
        public void PopulateSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someGuid2 = Guid.NewGuid();
            var data = new BoardTileSerializationData()
            {
                Id = someGuid.ToString(),
                BoardId = someGuid2.ToString(),
                Position = new QuadBoardPositionSerializationData()
                {
                    X = 3,
                    Y = 7
                }
            };

            sut.PopulateSerializationData(data);
            var pos = sut.Position as QuadBoardPosition;

            Assert.Equal(someGuid, sut.Id);
            Assert.Equal(3, pos.X);
            Assert.Equal(7, pos.Y);
        }
    }
}
