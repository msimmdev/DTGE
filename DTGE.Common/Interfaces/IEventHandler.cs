using System;
using System.Collections.Generic;
using System.Text;

namespace DTGE.Common.Interfaces
{
    public interface IEventHandler
    {
        void Listen<T>(Func<T, bool> handler) where T: IGameEvent;
        void Dispatch<T>(T gameEvent) where T: IGameEvent;
    }
}
