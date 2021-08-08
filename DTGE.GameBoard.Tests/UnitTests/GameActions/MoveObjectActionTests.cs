﻿using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using DTGE.GameBoard.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.Tests.UnitTests.GameActions
{
    public class MoveObjectActionTests
    {
        private Mock<IBoardObject> mockObject;
        private Mock<IBoardPosition> mockPosition;
        private MoveObjectAction sut;
        public MoveObjectActionTests()
        {
            mockObject = new Mock<IBoardObject>();
            mockObject.Setup(x => x.Equals(mockObject.Object)).Returns(true);
            mockPosition = new Mock<IBoardPosition>();
            mockPosition.Setup(x => x.Equals(mockPosition.Object)).Returns(true);
            sut = new MoveObjectAction(mockObject.Object, mockPosition.Object);
        }

        [Fact]
        public void Constructor_GetObject_ShouldReturnMatchingValue()
        {
            var tile = sut.Object;

            Assert.Same(mockObject.Object, tile);
        }

        [Fact]
        public void Constructor_GetPosition_ShouldReturnMatchingValue()
        {
            var pos = sut.NewPosition;

            Assert.Same(mockPosition.Object, pos);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(false);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_WithOrphanedObject_ShouldReturnError()
        {

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Validate_WithOccpiedPosition_ShouldReturnError()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.HasTile(It.IsAny<IBoardPosition>())).Returns(true);
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Execute_WithValidData_ShouldChangeTilePosition()
        {
            mockObject.SetupProperty(x => x.Position);

            sut.Execute();

            Assert.Same(mockPosition.Object, sut.Object.Position);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var tileGuid = Guid.NewGuid();
            var boardPositionDto = new BoardPositionDto();
            mockObject.SetupGet(x => x.Id).Returns(tileGuid);
            mockPosition.Setup(x => x.GetDto()).Returns(boardPositionDto);

            var result = sut.GetDto();
            var dto = result as MoveObjectActionDto;

            Assert.Equal(boardPositionDto.Position, dto.NewPosition.Position);
            Assert.Equal(sut.Id.ToString(), dto.Id);
            Assert.Equal(tileGuid.ToString(), dto.ObjectId);
        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var objectGuid = Guid.NewGuid();
            var mockPosition2 = new Mock<IBoardPosition>();
            mockPosition2.Setup(x => x.Equals(mockPosition.Object)).Returns(true);

            mockObject.SetupGet(x => x.Id).Returns(objectGuid);

            var resolver = new Mock<IObjectResolver>();
            resolver.Setup(x => x.Resolve<IBoardObject>(It.IsAny<Guid>())).Returns(mockObject.Object);
            resolver.Setup(x => x.Create<IBoardPosition>(It.IsAny<BoardPositionDto>())).Returns(mockPosition2.Object);

            var dto = new MoveObjectActionDto()
            {
                Id = idGuid.ToString(),
                ObjectId = objectGuid.ToString(),
                NewPosition = new BoardPositionDto(),
                Tags = new List<string>()
            };

            sut.UseDto(dto, resolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(objectGuid, sut.Object.Id);
            Assert.Same(mockPosition2.Object, sut.NewPosition);
        }
    }
}
