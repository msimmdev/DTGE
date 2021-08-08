using System;
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
    public class RemoveObjectActionTests
    {
        private Mock<IBoardObject> mockObject;
        private RemoveObjectAction sut;

        public RemoveObjectActionTests()
        {
            mockObject = new Mock<IBoardObject>();
            sut = new RemoveObjectAction(mockObject.Object);
        }

        [Fact]
        public void Constructor_GetTile_ShouldReturnMatchingValue()
        {
            var boardObject = sut.Object;

            Assert.Same(mockObject.Object, boardObject);
        }

        [Fact]
        public void Validate_WithValidData_ShouldReturnSuccess()
        {
            var mockBoard = new Mock<IBoard>();
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);

            var result = sut.Validate();

            Assert.True(result.Success);
        }

        [Fact]
        public void Validate_WithOrphanedTile_ShouldReturnError()
        {
            var result = sut.Validate();

            Assert.False(result.Success);
            Assert.NotEmpty(result.Error);
        }

        [Fact]
        public void Execute_WithValidData_ShouldDetechTile()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(x => x.Tiles).Returns(new Dictionary<Guid, IBoardTile>());
            mockObject.SetupProperty(x => x.Board);
            mockObject.Object.Board = mockBoard.Object;

            sut.Execute();

            Assert.Null(sut.Object.Board);
        }

        [Fact]
        public void Execute_WithValidData_ShouldRemovePosition()
        {
            var mockPosition = new Mock<IBoardPosition>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.SetupGet(x => x.Tiles).Returns(new Dictionary<Guid, IBoardTile>());
            mockObject.SetupGet(x => x.Board).Returns(mockBoard.Object);
            mockObject.SetupProperty(x => x.Position);
            mockObject.Object.Position = mockPosition.Object;

            sut.Execute();

            Assert.Null(sut.Object.Position);
        }

        [Fact]
        public void GetDto_FullyInitialized_ShouldReturnValidDto()
        {
            var objectGuid = Guid.NewGuid();
            mockObject.SetupGet(x => x.Id).Returns(objectGuid);

            var result = sut.GetDto();
            var dto = result as RemoveObjectActionDto;

            Assert.Equal(sut.Id.ToString(), dto.Id);
            Assert.Equal(objectGuid.ToString(), dto.ObjectId);
        }

        [Fact]
        public void UseDto_AllValues_ShouldSetValues()
        {
            var idGuid = Guid.NewGuid();
            var objectGuid = Guid.NewGuid();

            mockObject.SetupGet(x => x.Id).Returns(objectGuid);

            var resolver = new Mock<IObjectResolver>();
            resolver.Setup(x => x.Resolve<IBoardObject>(It.IsAny<Guid>())).Returns(mockObject.Object);

            var dto = new RemoveObjectActionDto()
            {
                Id = idGuid.ToString(),
                ObjectId = objectGuid.ToString(),
                Tags = new List<string>()
            };

            sut.UseDto(dto, resolver.Object);

            Assert.Equal(idGuid, sut.Id);
            Assert.Equal(objectGuid, sut.Object.Id);
        }
    }
}
