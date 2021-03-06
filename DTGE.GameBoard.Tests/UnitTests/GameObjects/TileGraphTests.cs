using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class TileGraphTests
    {
        private readonly TileGraph sut;
        public TileGraphTests()
        {
            sut = new TileGraph();
        }

        [Fact]
        public void Constructor_Empty_ShouldSetNonEmptyGuidId()
        {
            var id = sut.Id;

            Assert.IsType<Guid>(id);
            Assert.False(id == new Guid());
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
        public void Constructor_Empty_ShouldSetEmptyVertices()
        {
            var vertices = sut.Vertices;

            Assert.NotNull(vertices);
            Assert.Empty(vertices);
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
        public void AddVertex_CanAddAndGet_ShouldReturnSingleList()
        {
            var vertex = new Mock<IVertex<IBoardTile>>();
            vertex.Setup(v => v.Equals(vertex.Object)).Returns(true);

            sut.Vertices.Add(vertex.Object);
            var getVertex = sut.Vertices.First();

            Assert.Single(sut.Vertices);
            Assert.Equal(vertex.Object, getVertex);
        }

        [Fact]
        public void FindVertex_AddAndFind_ShouldReturnAddedVertex()
        {
            var tile = new Mock<IBoardTile>();
            var vertex = new Mock<IVertex<IBoardTile>>();
            tile.Setup(t => t.Equals(tile.Object)).Returns(true);
            vertex.Setup(v => v.Equals(vertex.Object)).Returns(true);
            vertex.SetupGet(v => v.Object).Returns(tile.Object);

            sut.Vertices.Add(vertex.Object);
            var getVertex = sut.FindVertex(tile.Object);

            Assert.Equal(vertex.Object, getVertex);
        }

        [Fact]
        public void FindVertex_EmptyVertices_ShouldReturnNull()
        {
            var tile = new Mock<IBoardTile>();

            var getVertex = sut.FindVertex(tile.Object);

            Assert.Null(getVertex);
        }

        [Fact]
        public void RemoveVertex_AddAndRemove_ShouldReturnEmptyVertices()
        {
            var vertex = new Mock<IVertex<IBoardTile>>();
            vertex.Setup(v => v.Equals(vertex.Object)).Returns(true);

            sut.Vertices.Add(vertex.Object);
            sut.Vertices.Remove(vertex.Object);
            var vertices = sut.Vertices;

            Assert.Empty(vertices);
        }

        [Fact]
        public void GetDto_CompleteData_ShouldMatchData()
        {
            var someGuid1 = Guid.NewGuid();
            var tile1 = new Mock<IBoardTile>();
            tile1.SetupGet(t => t.Id).Returns(someGuid1);

            var vertex1 = new Mock<IVertex<IBoardTile>>();
            vertex1.SetupGet(v => v.Object).Returns(tile1.Object);

            var someGuid2 = Guid.NewGuid();
            var tile2 = new Mock<IBoardTile>();
            tile2.SetupGet(t => t.Id).Returns(someGuid2);

            var vertex2 = new Mock<IVertex<IBoardTile>>();
            vertex2.SetupGet(v => v.Object).Returns(tile2.Object);

            var edge1 = new Mock<IEdge<IBoardTile>>();
            edge1.SetupGet(e => e.Distance).Returns(3);
            edge1.SetupGet(e => e.Source).Returns(vertex1.Object);
            edge1.SetupGet(e => e.Target).Returns(vertex2.Object);
            var edgeList1 = new List<IEdge<IBoardTile>>()
            {
                edge1.Object
            };

            vertex1.SetupGet(v => v.Edges).Returns(edgeList1);

            var edge2 = new Mock<IEdge<IBoardTile>>();
            edge2.SetupGet(e => e.Distance).Returns(5);
            edge2.SetupGet(e => e.Source).Returns(vertex2.Object);
            edge2.SetupGet(e => e.Target).Returns(vertex1.Object);
            var edgeList2 = new List<IEdge<IBoardTile>>()
            {
                edge2.Object
            };

            vertex2.SetupGet(v => v.Edges).Returns(edgeList2);

            sut.Vertices.Add(vertex1.Object);
            sut.Vertices.Add(vertex2.Object);
            var data = sut.GetDto();
            var objectData = data as TileGraphDto;
            var vertexData = objectData.Vertices.First();
            var edgeData = vertexData.Edges.First();

            Assert.Equal(sut.Id.ToString(), objectData.Id);
            Assert.Equal(2, objectData.Vertices.Count());
            Assert.Equal(someGuid1.ToString(), vertexData.ObjectId);
            Assert.Single(vertexData.Edges);
            Assert.Equal(3, edgeData.Distance);
            Assert.Equal(someGuid1.ToString(), edgeData.SourceObjectId);
            Assert.Equal(someGuid2.ToString(), edgeData.TargetObjectId);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var otherGuid1 = Guid.NewGuid();
            var tile1 = new Mock<IBoardTile>();
            tile1.Setup(t => t.Equals(tile1.Object)).Returns(true);
            var otherGuid2 = Guid.NewGuid();
            var tile2 = new Mock<IBoardTile>();
            tile2.Setup(t => t.Equals(tile1.Object)).Returns(true);
            var edge1 = new EdgeDto()
            {
                SourceObjectId = otherGuid1.ToString(),
                TargetObjectId = otherGuid2.ToString(),
                Distance = 4
            };
            var vertex1 = new VertexDto()
            {
                ObjectId = otherGuid1.ToString(),
                Edges = new List<EdgeDto>() { edge1 }
            };
            var vertex2 = new VertexDto()
            {
                ObjectId = otherGuid2.ToString(),
                Edges = new List<EdgeDto>()
            };
            var data = new TileGraphDto()
            {
                Id = someGuid.ToString(),
                Vertices = new List<VertexDto>() { vertex1, vertex2 }
            };

            sut.ObjectResolver = a =>
            {
                if (a == otherGuid1)
                {
                    return tile1.Object;
                } else if (a == otherGuid2)
                {
                    return tile2.Object;
                }
                throw new ArgumentException("Invalid GUID");
            };
            sut.UseDto(data, null);
            var id = sut.Id;
            var vertices = sut.Vertices;
            var firstVertex = vertices.First();
            var edges = firstVertex.Edges;
            var firstEdge = edges.First();

            Assert.Equal(someGuid, id);
            Assert.Equal(2, vertices.Count());
            Assert.Same(tile1.Object, firstVertex.Object);
            Assert.Single(edges);
            Assert.Equal(4, firstEdge.Distance);
            Assert.Equal(tile1.Object, firstEdge.Source.Object);
            Assert.Equal(tile2.Object, firstEdge.Target.Object);
        }

        [Fact]
        public void Equals_OtherGraph_ShouldReturnFalse()
        {
            var otherSut = new TileGraph();

            var result = sut.Equals(otherSut as IdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherGraphInterface_ShouldReturnFalse()
        {
            var otherSut = new TileGraph();

            var result = sut.Equals(otherSut as IIdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherObject_ShouldReturnFalse()
        {
            var otherSut = new TileGraph();

            var result = sut.Equals(otherSut as Object);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_OtherObject_ShouldNotMatch()
        {
            var otherSut = new TileGraph();

            var result = sut.GetHashCode() == otherSut.GetHashCode();

            Assert.False(result);
        }
    }
}
