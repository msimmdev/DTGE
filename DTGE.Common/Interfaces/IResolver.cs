using System;

namespace DTGE.Common.Interfaces
{
    public interface IResolver
    {
        IIdentifiedObject Resolve(Guid id);
        T Resolve<T>(Guid id);
        T Create<T>();
        T Create<T>(IGameDto data);
    }
}
