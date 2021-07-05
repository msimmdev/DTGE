﻿using System;
using Xunit;
using Moq;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using System.Collections.Generic;

namespace DTGE.GameBoard.Tests.UnitTests.GameObjects
{
    public class BoardTests
    {
        private Board sut;
        public BoardTests()
        {
            sut = new Board();
        }

        [Fact]
        public void Constructor_Empty_ShouldSetNonEmptyGuidId()
        {
            Assert.IsType<Guid>(sut.Id);
            Assert.False(sut.Id == new Guid());
        }

        [Fact]
        public void Tiles_GetAndSet_ShouldMatch()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();

            sut.Tiles.Add(someGuid, tile.Object);

            Assert.Equal(tile.Object, sut.Tiles[someGuid]);
        }

        [Fact]
        public void Objects_GetAndSet_ShouldMatch()
        {
            var someGuid = Guid.NewGuid();
            var boardObject = new Mock<IBoardObject>();

            sut.Objects.Add(someGuid, boardObject.Object);

            Assert.Equal(boardObject.Object, sut.Objects[someGuid]);
        }

        [Fact]
        public void HasTile_MatchingMockData_ShouldReturnTrue()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(true);
            tile.SetupGet(x => x.Position).Returns(pos.Object);

            sut.Tiles.Add(someGuid, tile.Object);
            var hasTile = sut.HasTile(pos.Object);

            Assert.True(hasTile);
        }

        [Fact]
        public void HasTile_NonMatchingMockData_ShouldReturnFalse()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(false);
            tile.SetupGet(x => x.Position).Returns(pos.Object);

            sut.Tiles.Add(someGuid, tile.Object);
            var hasTile = sut.HasTile(pos.Object);

            Assert.False(hasTile);
        }

        [Fact]
        public void FindTile_MatchingMockData_ShouldReturnTile()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(true);
            tile.SetupGet(x => x.Position).Returns(pos.Object);

            sut.Tiles.Add(someGuid, tile.Object);
            var foundTile = sut.FindTile(pos.Object);

            Assert.Equal(tile.Object, foundTile);
        }

        [Fact]
        public void FindTile_NonMatchingMockData_ShouldThrowException()
        {
            var someGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            var pos = new Mock<IBoardPosition>();
            pos.Setup(x => x.Equals(pos.Object)).Returns(false);
            tile.SetupGet(x => x.Position).Returns(pos.Object);

            sut.Tiles.Add(someGuid, tile.Object);

            Assert.Throws<InvalidOperationException>(delegate { sut.FindTile(pos.Object); });
        }

        [Fact]
        public void GetSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someOtherGuid = Guid.NewGuid();
            var tile = new Mock<IBoardTile>();
            tile.SetupGet(x => x.Id).Returns(someGuid);
            var boardObject = new Mock<IBoardObject>();
            boardObject.SetupGet(x => x.Id).Returns(someOtherGuid);

            sut.Tiles.Add(someGuid, tile.Object);
            sut.Objects.Add(someOtherGuid, boardObject.Object);
            var data = sut.GetSerializationData();
            var boardData = data as BoardSerializationData;

            Assert.IsAssignableFrom<BoardSerializationData>(data);
            Assert.Equal(sut.Id.ToString(), boardData.Id);
            Assert.Equal(someGuid.ToString(), boardData.BoardTileIds[0]);
            Assert.Equal(someOtherGuid.ToString(), boardData.ObjectIds[0]);
        }

        [Fact]
        public void PopulateSerializationData_CompleteData_ShouldMatchData()
        {
            var someGuid = Guid.NewGuid();
            var someGuid2 = Guid.NewGuid();
            var someGuid3 = Guid.NewGuid();
            var data = new BoardSerializationData()
            {
                Id = someGuid.ToString(),
                BoardTileIds = new List<string>() { someGuid2.ToString() },
                ObjectIds = new List<string>() { someGuid3.ToString() },
            };

            sut.PopulateSerializationData(data);

            Assert.Equal(someGuid, sut.Id);
        }
    }
}
