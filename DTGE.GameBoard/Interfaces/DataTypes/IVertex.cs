using System;
using System.Collections.Generic;

namespace DTGE.GameBoard.Interfaces.DataTypes
{
    public interface IVertex<T> : IEquatable<IVertex<T>>
    {
        T Object { get; set; }
        IEnumerable<IEdge<T>> Edges { get; }

        bool IsAdjacent(IVertex<T> other);
        IEnumerable<IVertex<T>> Neighbors();
        void AddEdge(IEdge<T> edge);
        void RemoveEdge(IEdge<T> edge);
    }
}
