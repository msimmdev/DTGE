using DTGE.Common.Interfaces;
using System;

namespace DTGE.Common.Core
{
    public class EmptyState : IGameState, IEquatable<EmptyState>
    {
        public bool Equals(IGameState other)
        {
            return other.GetType() == typeof(EmptyState);
        }

        public bool Equals(EmptyState other)
        {
            return true;
        }

        public IGameSerializationData GetSerializationData()
        {
            return new EmptySerializationData();
        }

        public void PopulateSerializationData(IGameSerializationData data)
        {
        }
    }
}
