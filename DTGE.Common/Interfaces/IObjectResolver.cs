using System;
using System.Collections.Generic;
using System.Text;

namespace DTGE.Common.Interfaces
{
    public interface IObjectResolver
    {
        IIdentifiedObject Resolve(Guid id);
        T Resolve<T>(Guid id);
        T Create<T>(IGameDto data);
    }
}
