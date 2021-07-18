using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    /// <summary>
    /// An unordered set of tiles representing a field of tiles, can be used to represent possible pathing targets.
    /// </summary>
    public interface ITileField : IGameSerializable
    {
        Guid Id { get; set; }
        ISet<IBoardTile> Tiles { get; set; }
    }
}
