using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class TileFieldTests
    {
        private readonly TileField sut;
        public TileFieldTests()
        {
            sut = new TileField();
        }

        [Fact]
        public void Constructor_Empty_ShouldSetNonEmptyGuidId()
        {
            var id = sut.Id;

            Assert.IsType<Guid>(id);
            Assert.False(id == new Guid());
        }

        [Fact]
        public void Constructor_Empty_ShouldSetEmptyTiles()
        {
            var tiles = sut.Tiles;

            Assert.NotNull(tiles);
            Assert.Empty(tiles);
        }

        [Fact]
        public void AddTile_CanAddAndGet_ShouldReturnSingleList()
        {
            var tile = new Mock<IBoardTile>();
            tile.Setup(v => v.Equals(tile.Object)).Returns(true);

            sut.Tiles.Add(tile.Object);
            var getTile = sut.Tiles.First();

            Assert.Single(sut.Tiles);
            Assert.Equal(tile.Object, getTile);
        }

        [Fact]
        public void GetSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.Setup(t => t.Equals(tile.Object)).Returns(true);
            tile.SetupGet(t => t.Id).Returns(someGuid);
            sut.Tiles.Add(tile.Object);

            var data = sut.GetSerializationData();
            var objectData = data as TileFieldSerializationData;
            var tileId = objectData.TileIds.First();

            Assert.Equal(sut.Id.ToString(), objectData.Id);
            Assert.Single(objectData.TileIds);
            Assert.Equal(someGuid.ToString(), tileId);
        }

        [Fact]
        public void PopulateSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.SetupGet(t => t.Id).Returns(otherGuid);
            var tileIds = new List<string>() { otherGuid.ToString() };
            var data = new TileFieldSerializationData()
            {
                Id = someGuid.ToString(),
                TileIds = tileIds
            };

            sut.ObjectResolver = i => { return tile.Object; };
            sut.PopulateSerializationData(data);
            var gotTile = sut.Tiles.First();

            Assert.Equal(someGuid, sut.Id);
            Assert.Single(sut.Tiles);
            Assert.Same(tile.Object, gotTile);
        }
    }
}
