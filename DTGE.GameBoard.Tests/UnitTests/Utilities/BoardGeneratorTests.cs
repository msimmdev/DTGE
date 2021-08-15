using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using DTGE.GameBoard.Utilities;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Tests.UnitTests.Utilities
{
    public class BoardGeneratorTests
    {
        [Fact]
        public void GenerateBoardFromShape_MockShape_ShouldReturnValidBoard()
        {
            var mockResolver = new Mock<IResolver>();
            var mockShape = new Mock<IBoardShape>();
            var mockBoard = new Mock<IBoard>();
            var mockTile = new Mock<IBoardTile>();

            mockResolver.Setup(x => x.Create<IBoard>()).Returns(mockBoard.Object);
            mockResolver.Setup(x => x.Create<IBoardTile>()).Returns(mockTile.Object);

            var result = BoardGenerator.GenerateBoardFromShape(mockResolver.Object, mockShape.Object);

            Assert.Same(mockBoard.Object, result);
        }

        [Fact]
        public void GenerateBoardFromShape_MockShape_ShouldCreateCorrectTiles()
        {
            var mockResolver = new Mock<IResolver>();
            var mockShape = new Mock<IBoardShape>();
            var mockBoard = new Mock<IBoard>();

            mockBoard.SetupProperty(x => x.Tiles);
            mockBoard.Object.Tiles = new Dictionary<Guid, IBoardTile>();

            var pos1 = new Mock<IBoardPosition>();
            pos1.Setup(x => x.Equals(pos1.Object)).Returns(true);
            var pos2 = new Mock<IBoardPosition>();
            pos2.Setup(x => x.Equals(pos2.Object)).Returns(true);
            var pos3 = new Mock<IBoardPosition>();
            pos3.Setup(x => x.Equals(pos3.Object)).Returns(true);

            var posList = new List<IBoardPosition>()
            {
                pos1.Object,
                pos2.Object,
                pos3.Object,
            };

            mockShape.SetupGet(x => x.Positions).Returns(posList);

            mockResolver.Setup(x => x.Create<IBoard>()).Returns(mockBoard.Object);
            mockResolver.Setup(x => x.Create<IBoardTile>()).Returns(delegate
            {
                var t = new Mock<IBoardTile>();
                t.SetupAllProperties();
                t.Object.Id = Guid.NewGuid();
                return t.Object;
            });

            var result = BoardGenerator.GenerateBoardFromShape(mockResolver.Object, mockShape.Object);

            var res = result.Tiles.Values.Select(x => x.Position);

            Assert.Contains(pos1.Object, res);
            Assert.Contains(pos2.Object, res);
            Assert.Contains(pos3.Object, res);
        }
    }
}
