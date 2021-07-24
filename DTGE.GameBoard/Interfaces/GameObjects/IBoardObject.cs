using System;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.Behaviour;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// Representation of an object that can be placed onto a board at a position.
    /// </summary>
    public interface IBoardObject : IPositionable, IIdentifiedObject
    {
        /// <summary>
        /// A reference to the board the object is placed upon.
        /// </summary>
        IBoard Board { get; set; }
    }
}