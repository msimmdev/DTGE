using System;
using System.Collections.Generic;
using System.Text;

namespace DTGE.Common.Interfaces
{
    public interface IGameEvent
    {

    }

    public interface IGameEvent<T> : IGameEvent where T : IIdentifiedAction
    {
        T GameAction { get; }
    }
}
