using System;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.Behaviour;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// Representation of an object that can be placed onto a board at a position.
    /// </summary>
    public interface IBoardObject : IPositionable, IGameSerializable
    {
        /// <summary>
        /// A unique ID for the object.
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// A reference to the board the object is placed upon.
        /// </summary>
        IBoard Board { get; set; }
    }
}