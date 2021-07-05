using System;
using DTGE.Common.Interfaces;

namespace DTGE.Common.Base
{
    public abstract class DTGEObject : IEquatable<DTGEObject>
    {
        public Guid Id { get; set; }

        public override bool Equals(Object other)
        {
            return Equals(other as DTGEObject);
        }

        public bool Equals(DTGEObject other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
