using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// An ordered list of tiles representing a path between two points.
    /// </summary>
    public interface ITilePath : IGameSerializable
    {
        Guid Id { get; set; }
        IList<IBoardTile> Tiles { get; set; }
        int Distance { get; set; }
    }
}
