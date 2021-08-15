using System;
using System.Collections.Generic;
using DTGE.Common.Core;

namespace DTGE.Common.Interfaces
{
    public interface IIdentifiedAction : IEquatable<IIdentifiedAction>, IGameSerializable
    {
        Guid Id { get; set; }
        ISet<string> Tags { get; set; }
        bool Completed { get; }

        void Execute(IEventHandler handler);
        ValidationResult Validate();
    }
}
