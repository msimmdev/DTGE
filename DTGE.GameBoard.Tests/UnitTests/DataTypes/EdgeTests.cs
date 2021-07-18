using Xunit;
using Moq;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.DataTypes;

namespace DTGE.GameBoard.Tests.UnitTests.DataTypes
{
    public class EdgeTests
    {
        [Fact]
        public void Source_CanGet_ShouldMatchConstructor()
        {
            var source = new Mock<IVertex<IBoardTile>>();
            source.Setup(x => x.Equals(source.Object)).Returns(true);
            var target = new Mock<IVertex<IBoardTile>>();
            var distance = 17;

            var sut = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var check = sut.Source;

            Assert.Equal(source.Object, check);
        }

        [Fact]
        public void Target_CanGet_ShouldMatchConstructor()
        {
            var source = new Mock<IVertex<IBoardTile>>();
            var target = new Mock<IVertex<IBoardTile>>();
            target.Setup(x => x.Equals(target.Object)).Returns(true);
            var distance = 17;

            var sut = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var check = sut.Target;

            Assert.Equal(target.Object, check);
        }

        [Fact]
        public void Distance_CanGet_ShouldMatchConstructor()
        {
            var source = new Mock<IVertex<IBoardTile>>();
            var target = new Mock<IVertex<IBoardTile>>();
            var distance = 17;

            var sut = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var check = sut.Distance;

            Assert.Equal(distance, check);
        }

        [Fact]
        public void Equals_MatchingValues_ShouldReturnTrue()
        {
            var source = new Mock<IVertex<IBoardTile>>();
            source.Setup(x => x.Equals(source.Object)).Returns(true);
            var target = new Mock<IVertex<IBoardTile>>();
            target.Setup(x => x.Equals(target.Object)).Returns(true);
            var distance = 17;

            var sut1 = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var sut2 = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var check = sut1.Equals(sut2);

            Assert.True(check);
        }

        [Fact]
        public void Equals_NonMatchingValues_ShouldReturnFalse()
        {
            var source = new Mock<IVertex<IBoardTile>>();
            source.Setup(x => x.Equals(source.Object)).Returns(false);
            var target = new Mock<IVertex<IBoardTile>>();
            target.Setup(x => x.Equals(target.Object)).Returns(false);
            var distance = 17;

            var sut1 = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var sut2 = new Edge<IBoardTile>(source.Object, target.Object, distance);
            var check = sut1.Equals(sut2);

            Assert.False(check);
        }

    }
}
