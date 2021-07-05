using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.DataTypes;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Tests.UnitTests.DataTypes
{
    public class BoardShapeTests
    {
        [Fact]
        public void Positions_GetAndSet_ShouldMatch()
        {
            var sut = new BoardShape();
            var posList = new List<IBoardPosition>(){
                new Mock<IBoardPosition>().Object,
                new Mock<IBoardPosition>().Object,
                new Mock<IBoardPosition>().Object
            };

            sut.Positions = posList;

            Assert.Equal(posList, sut.Positions);
        }
    }
}
