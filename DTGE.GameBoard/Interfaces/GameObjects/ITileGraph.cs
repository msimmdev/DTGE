using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// A graph of tiles made up for vertex's linked to a tile and edges between vertex's. Can be used to to generate fields and paths to navigate a board.
    /// </summary>
    public interface ITileGraph : IIdentifiedObject
    {
        ISet<IVertex<IBoardTile>> Vertices { get; }
        IVertex<IBoardTile> FindVertex(IBoardTile tile);
    }
}
