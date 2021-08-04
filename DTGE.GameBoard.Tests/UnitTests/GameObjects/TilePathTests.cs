using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class TilePathTests
    {
        private readonly TilePath sut;
        public TilePathTests()
        {
            sut = new TilePath();
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
        public void Constructor_Empty_ShouldSetEmptyTiles()
        {
            var tiles = sut.Tiles;

            Assert.NotNull(tiles);
            Assert.Empty(tiles);
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
        public void Tiles_CanAddAndGet_ShouldReturnSingleList()
        {
            var tile = new Mock<IBoardTile>();
            tile.Setup(v => v.Equals(tile.Object)).Returns(true);

            sut.Tiles.Add(tile.Object);
            var getTile = sut.Tiles.First();

            Assert.Single(sut.Tiles);
            Assert.Equal(tile.Object, getTile);
        }

        [Fact]
        public void Distance_CanSetAndGet_ShouldMatchData()
        {
            sut.Distance = 4;

            Assert.Equal(4, sut.Distance);
        }

        [Fact]
        public void GetDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.Setup(t => t.Equals(tile.Object)).Returns(true);
            tile.SetupGet(t => t.Id).Returns(someGuid);
            sut.Tiles.Add(tile.Object);
            sut.Distance = 6;

            var data = sut.GetDto();
            var objectData = data as TilePathDto;
            var tileId = objectData.TileIds.First();

            Assert.Equal(sut.Id.ToString(), objectData.Id);
            Assert.Single(objectData.TileIds);
            Assert.Equal(someGuid.ToString(), tileId);
            Assert.Equal(6, objectData.Distance);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.SetupGet(t => t.Id).Returns(otherGuid);
            var tileIds = new List<string>() { otherGuid.ToString() };
            var data = new TilePathDto()
            {
                Id = someGuid.ToString(),
                TileIds = tileIds,
                Distance = 7
            };

            sut.ObjectResolver = i => { return tile.Object; };
            sut.UseDto(data, null);
            var gotTile = sut.Tiles.First();

            Assert.Equal(someGuid, sut.Id);
            Assert.Single(sut.Tiles);
            Assert.Same(tile.Object, gotTile);
            Assert.Equal(7, sut.Distance);
        }
    }
}