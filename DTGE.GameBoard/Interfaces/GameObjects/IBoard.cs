using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// Representation of a board.
    /// </summary>
    public interface IBoard : IIdentifiedObject
    {
        /// <summary>
        /// A lookup dictionary of the tiles that make up the board.
        /// </summary>
        IDictionary<Guid, IBoardTile> Tiles { get; set; }
        /// <summary>
        /// A lookup dictionary of the objects placed onto the board.
        /// </summary>
        IDictionary<Guid, IBoardObject> Objects { get; set; }
        /// <summary>
        /// Check to see if a tile exists at a given position.
        /// </summary>
        /// <param name="position">A valid board position</param>
        /// <returns>true if a tile exists, false if it does not.</returns>
        bool HasTile(IBoardPosition position);
        /// <summary>
        /// Return the tile at the given position.
        /// </summary>
        /// <param name="position">A valid board position</param>
        /// <returns>The tile if one exists, throws exception if not.</returns>
        IBoardTile FindTile(IBoardPosition position);
    }
}
