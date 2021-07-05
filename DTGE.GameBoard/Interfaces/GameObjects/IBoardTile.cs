using System;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.Behaviour;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// Representation of a tile at a given position on a board.
    /// </summary>
    public interface IBoardTile : IPositionable, IGameSerializable
    {
        /// <summary>
        /// A unique ID for the tile.
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// A reference to the board the tile is part of.
        /// </summary>
        IBoard Board { get; set; }
    }
}