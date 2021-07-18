using System.Linq;
using Xunit;
using Moq;
using DTGE.GameBoard.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Tests.UnitTests.DataTypes
{
    public class VertexTests
    {
        private Mock<IBoardTile> tile;
        private Vertex<IBoardTile> sut;
        public VertexTests()
        {
            tile = new Mock<IBoardTile>();
            sut = new Vertex<IBoardTile>(tile.Object);
        }

        [Fact]
        public void Constructor_CreateAndGet_ShouldMatch()
        {
            Assert.Equal(tile.Object, sut.Object);
        }

        [Fact]
        public void AddEdge_CanSetAndGet_ShouldMatch()
        {
            var edge = new Mock<IEdge<IBoardTile>>();
            edge.Setup(e => e.Equals(edge.Object)).Returns(true);

            sut.AddEdge(edge.Object);

            Assert.Single(sut.Edges);
            Assert.Equal(edge.Object, sut.Edges.First());
        }

        [Fact]
        public void Equals_MatchingObject_ShouldReturnTrue()
        {
            var second = new Vertex<IBoardTile>(tile.Object);

            var result = sut.Equals(second);

            Assert.True(result);
        }

        [Fact]
        public void Equals_MatchingObject_ShouldReturnFalse()
        {
            var newTile = new Mock<IBoardTile>();
            var second = new Vertex<IBoardTile>(newTile.Object);

            var result = sut.Equals(second);

            Assert.False(result);
        }

        [Fact]
        public void IsAdjacent_WithEdge_ShouldReturnTrue()
        {
            var newTile = new Mock<IBoardTile>();
            var second = new Vertex<IBoardTile>(newTile.Object);
            var edge = new Mock<IEdge<IBoardTile>>();
            edge.SetupGet(e => e.Target).Returns(second);

            sut.AddEdge(edge.Object);
            var result = sut.IsAdjacent(second);

            Assert.True(result);
        }

        [Fact]
        public void IsAdjacent_NoEdge_ShouldReturnFalse()
        {
            var newTile = new Mock<IBoardTile>();
            var second = new Vertex<IBoardTile>(newTile.Object);

            var result = sut.IsAdjacent(second);

            Assert.False(result);
        }

        [Fact]
        public void Neighbors_WithEdge_ShouldReturnVertex()
        {
            var newTile = new Mock<IBoardTile>();
            var second = new Vertex<IBoardTile>(newTile.Object);
            var edge = new Mock<IEdge<IBoardTile>>();
            edge.SetupGet(e => e.Target).Returns(second);

            sut.AddEdge(edge.Object);
            var neighbors = sut.Neighbors();

            Assert.Single(neighbors);
            Assert.Equal(second, neighbors.First());
        }

        [Fact]
        public void RemoveEdge_CanRemoveAndGetEdges_ShouldBeEmpty()
        {
            var edge = new Mock<IEdge<IBoardTile>>();
            edge.Setup(e => e.Equals(edge.Object)).Returns(true);

            sut.AddEdge(edge.Object);
            sut.RemoveEdge(edge.Object);

            Assert.Empty(sut.Edges);
        }
    }
}
