using Xunit;
using Moq;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;

namespace DTGE.Common.Tests.Core
{
    public class ActionStartEventTests
    {
        private ActionStartEvent<IIdentifiedAction> sut;
        private Mock<IIdentifiedAction> mockAction;
        public ActionStartEventTests()
        {
            mockAction = new Mock<IIdentifiedAction>();
            sut = new ActionStartEvent<IIdentifiedAction>(mockAction.Object);
        }

        [Fact]
        public void Constructor_CanGetGameAction_ShouldReturnAction()
        {
            var result = sut.GameAction;

            Assert.Same(mockAction.Object, result);
        }
    }
}
