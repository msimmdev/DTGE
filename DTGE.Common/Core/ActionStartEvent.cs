using DTGE.Common.Interfaces;

namespace DTGE.Common.Core
{
    public class ActionStartEvent<T> : IGameEvent<T> where T : IIdentifiedAction
    {
        public ActionStartEvent(T gameAction)
        {
            GameAction = gameAction;
        }
        public T GameAction { get; }
    }
}
