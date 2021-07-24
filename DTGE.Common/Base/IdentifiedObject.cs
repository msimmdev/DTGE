using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.Common.Base
{
    public abstract class IdentifiedObject : IEquatable<IdentifiedObject>
    {
        public IdentifiedObject()
        {
            Id = Guid.NewGuid();
            Tags = new HashSet<string>();
        }

        public Guid Id { get; set; }
        public ISet<string> Tags { get; set; }

        public override bool Equals(Object other)
        {
            return Equals(other as IdentifiedObject);
        }

        public bool Equals(IIdentifiedObject other)
        {
            return Id.Equals(other.Id);
        }

        public bool Equals(IdentifiedObject other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
