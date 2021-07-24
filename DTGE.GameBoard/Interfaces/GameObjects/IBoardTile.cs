using System;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.Behaviour;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// Representation of a tile at a given position on a board.
    /// </summary>
    public interface IBoardTile : IPositionable, IIdentifiedObject
    {
        /// <summary>
        /// A reference to the board the tile is part of.
        /// </summary>
        IBoard Board { get; set; }
    }
}