using System;
using System.Collections.Generic;
using DTGE.Common.Core;

namespace DTGE.Common.Interfaces
{
    public interface IIdentifiedAction : IEquatable<IIdentifiedAction>, IGameSerializable
    {
        Guid Id { get; set; }
        ISet<string> Tags { get; set; }
        Action PreExecute { get; set; }
        Action PostExecute { get; set; }
        bool Completed { get; }

        void Execute();
        void Execute(Action preExecute, Action postExecute);
        ValidationResult Validate();
    }
}
