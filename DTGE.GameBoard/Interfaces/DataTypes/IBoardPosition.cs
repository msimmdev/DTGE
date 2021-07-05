using DTGE.Common.Interfaces;
using System;

namespace DTGE.GameBoard.Interfaces.DataTypes
{
    /// <summary>
    /// A type that describes a specific position on a board.
    /// </summary>
    public interface IBoardPosition : IEquatable<IBoardPosition>, IGameSerializable
    {
    }
}
