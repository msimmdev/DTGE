using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;

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
        public void GetDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.Setup(t => t.Equals(tile.Object)).Returns(true);
            tile.SetupGet(t => t.Id).Returns(someGuid);
            sut.Tiles.Add(tile.Object);

            var data = sut.GetDto();
            var objectData = data as TileFieldDto;
            var tileId = objectData.TileIds.First();

            Assert.Equal(sut.Id.ToString(), objectData.Id);
            Assert.Single(objectData.TileIds);
            Assert.Equal(someGuid.ToString(), tileId);
        }

        [Fact]
        public void UseDto_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var otherGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.SetupGet(t => t.Id).Returns(otherGuid);
            var tileIds = new List<string>() { otherGuid.ToString() };
            var data = new TileFieldDto()
            {
                Id = someGuid.ToString(),
                TileIds = tileIds
            };

            sut.ObjectResolver = i => { return tile.Object; };
            sut.UseDto(data, null);
            var gotTile = sut.Tiles.First();

            Assert.Equal(someGuid, sut.Id);
            Assert.Single(sut.Tiles);
            Assert.Same(tile.Object, gotTile);
        }

        [Fact]
        public void Equals_OtherField_ShouldReturnFalse()
        {
            var otherSut = new TileField();

            var result = sut.Equals(otherSut as IdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherFieldInterface_ShouldReturnFalse()
        {
            var otherSut = new TileField();

            var result = sut.Equals(otherSut as IIdentifiedObject);

            Assert.False(result);
        }

        [Fact]
        public void Equals_OtherObject_ShouldReturnFalse()
        {
            var otherSut = new TileField();

            var result = sut.Equals(otherSut as Object);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_OtherObject_ShouldNotMatch()
        {
            var otherSut = new TileField();

            var result = sut.GetHashCode() == otherSut.GetHashCode();

            Assert.False(result);
        }
    }
}
