using DTGE.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
