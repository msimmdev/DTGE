using System;
using System.Collections.Generic;
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
        public Action PreExecute { get; set; }
        public Action PostExecute { get; set; }
        public bool Completed { get; protected set; }

        public override bool Equals(Object other)
        {
            return Equals(other as IdentifiedObject);
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

        public virtual void Execute(Action preExecute, Action postExecute)
        {
            if (preExecute != null)
                preExecute();
            PerformAction();
            if (postExecute != null)
                postExecute();
        }

        public virtual void Execute()
        {
            Execute(PreExecute, PostExecute);
        }

        protected abstract void PerformAction();
    }
}