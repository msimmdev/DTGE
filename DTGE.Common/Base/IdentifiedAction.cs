using System;
using System.Collections.Generic;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;

namespace DTGE.Common.Base
{
    public abstract class IdentifiedAction : IEquatable<IdentifiedAction>
    {
        public IdentifiedAction()
        {
            Id = Guid.NewGuid();
            Tags = new HashSet<string>();
            Completed = false;
        }

        public Guid Id { get; set; }
        public ISet<string> Tags { get; set; }
        public bool Completed { get; protected set; }

        public override bool Equals(Object other)
        {
            return Equals(other as IIdentifiedAction);
        }

        public bool Equals(IIdentifiedAction other)

        {
            return Id.Equals(other.Id);
        }

        public bool Equals(IdentifiedAction other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}