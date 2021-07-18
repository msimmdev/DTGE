using System;

namespace DTGE.GameBoard.Interfaces.DataTypes
{
    public interface IEdge<T> : IEquatable<IEdge<T>>
    {
        IVertex<T> Source { get; }
        IVertex<T> Target { get; }
        int Distance { get; set; }
    }
}
